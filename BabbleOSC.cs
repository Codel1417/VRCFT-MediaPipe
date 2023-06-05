using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using VRCFaceTracking.Core.OSC;
using VRCFaceTracking.Core.Params.Expressions;

namespace VRCFaceTracking.Babble;
public partial class BabbleOSC
{
    private bool _loop;
    private UdpClient _receiver;
    private IPEndPoint _endpoint;
    private readonly Thread _thread;
    private readonly ILogger _logger;
    private readonly int _resolvedPort;
    private const int DEFAULT_PORT = 8888;

    public BabbleOSC(ILogger iLogger, int? port = null)
    {   
        _logger = iLogger;
        _loop = true;
        _resolvedPort = port ?? DEFAULT_PORT;
        _receiver = new UdpClient(_resolvedPort);
        _endpoint = new IPEndPoint(IPAddress.Loopback, _resolvedPort);
        _thread = new Thread(new ThreadStart(ListenLoop));
        _thread.Start();
    }

    private void ListenLoop()
    {
        OscMessageMeta oscMeta = new OscMessageMeta();
        byte[] buffer;

        while (_loop)
        {
            try
            {
                buffer = _receiver.Receive(ref _endpoint);
                if (SROSCLib.parse_osc(buffer, buffer.Length, ref oscMeta))
                {
                    if (oscMeta.Type != OscValueType.Float) continue; // Possibly redundant

                    float value = oscMeta.Value.FloatValues[0];
                    if (BabbleUniqueExpressionMap.ContainsKey2(oscMeta.Address))
                    {
                        BabbleUniqueExpressionMap.SetByKey2(oscMeta.Address, value);
                        continue;
                    }

                    switch (oscMeta.Address)
                    {
                        case "/mouthFunnel":
                            BabbleUniqueExpressionMap.SetByKey1(UnifiedExpressions.LipFunnelLowerLeft, value);
                            BabbleUniqueExpressionMap.SetByKey1(UnifiedExpressions.LipFunnelLowerRight, value);
                            BabbleUniqueExpressionMap.SetByKey1(UnifiedExpressions.LipFunnelUpperLeft, value);
                            BabbleUniqueExpressionMap.SetByKey1(UnifiedExpressions.LipFunnelUpperRight, value);
                            break;
                        case "/mouthPucker":
                            BabbleUniqueExpressionMap.SetByKey1(UnifiedExpressions.LipPuckerLowerLeft, value);
                            BabbleUniqueExpressionMap.SetByKey1(UnifiedExpressions.LipPuckerLowerRight, value);
                            BabbleUniqueExpressionMap.SetByKey1(UnifiedExpressions.LipPuckerUpperLeft, value);
                            BabbleUniqueExpressionMap.SetByKey1(UnifiedExpressions.LipPuckerUpperRight, value);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }

    public void Teardown()
    {
        _loop = false;
        _receiver.Close();
        _thread.Join();
    }
}
