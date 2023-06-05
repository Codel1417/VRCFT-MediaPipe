using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using VRCFaceTracking.Core.OSC;

namespace VRCFaceTracking.Babble;
public partial class BabbleOSC
{
    // Workaround for duplicate keys in the OSC map
    public float MouthFunnel { get; private set; }
    public float MouthPucker { get; private set; }

    private readonly Socket _receiver;
    private readonly Thread _thread;
    private readonly ILogger _logger;
    private readonly int _resolvedPort;
    private const int DEFAULT_PORT = 8888;
    public BabbleOSC(ILogger iLogger, int? port = null)
    {   
        _logger = iLogger;

        if (_receiver != null)
        {
            _logger.LogError("BabbleOSC connection already exists.");
            return;
        }

        _receiver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _resolvedPort = port ?? DEFAULT_PORT;
        _receiver.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), _resolvedPort));
        _thread = new Thread(new ThreadStart(ListenLoop));
        _thread.Start();
    }

    private void ListenLoop()
    {
        OscMessageMeta oscMeta = new OscMessageMeta();
        var buffer = new byte[4096];

        while (!MainStandalone.MasterCancellationTokenSource.IsCancellationRequested)
        {
            try
            {
                var length = _receiver.Receive(buffer);
                if (SROSCLib.parse_osc(buffer, length, ref oscMeta))
                {
                    if (BabbleUniqueExpressionMap.ContainsKey2(oscMeta.Address))
                    {
                        if (oscMeta.Type != OscValueType.Float) // Possibly redundant
                        {
                            continue;
                        }

                        float value = oscMeta.Value.FloatValues[0];
                        switch (oscMeta.Address)
                        {
                            case "/mouthFunnel":
                                MouthFunnel = value;
                                break;
                            case "/mouthPucker":
                                MouthPucker = value;
                                break;
                            default:
                                BabbleUniqueExpressionMap.SetByKey2(oscMeta.Address, value);
                                break;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                if (_receiver.Connected)
                    _logger.LogError(e.Message);
            }
        }
    }

    public void Teardown()
    {
        _receiver.Close();
        _receiver.Dispose();
        _thread.Join();
    }
}
