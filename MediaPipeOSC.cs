using System.Net;
using System.Net.Sockets;
using VRCFaceTracking.Core.OSC;

namespace VRCFaceTracking.MediaPipe;
public partial class MediaPipeOSC
{
    private Socket _receiver;
    private bool _loop = true;
    private readonly Thread _thread;
    private readonly int _resolvedPort;
    private const int DEFAULT_PORT = 8888;
    private const int TIMEOUT_MS = 10_000;

    public MediaPipeOSC(int? port = null)
    {   
 

        _receiver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _resolvedPort = port ?? DEFAULT_PORT;
        _receiver.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), _resolvedPort));
        _receiver.ReceiveTimeout = TIMEOUT_MS;

        _loop = true;
        _thread = new Thread(new ThreadStart(ListenLoop));
        _thread.Start();
    }

    private void ListenLoop()
    {
        OscMessageMeta oscMeta = new OscMessageMeta();
        var buffer = new byte[4096];

        while (_loop)
        {
            try
            {
                if (_receiver.IsBound)
                {
                    var length = _receiver.Receive(buffer);
                    if (SROSCLib.parse_osc(buffer, length, ref oscMeta))
                    {
                        if (BabbleExpressionMap.ContainsKey(oscMeta.Address))
                        {
                            if (oscMeta.Type == OscValueType.Float) // Possibly redundant
                            {
                                BabbleExpressionMap[oscMeta.Address] = oscMeta.Value.FloatValues[0];
                            }
                        }
                    }
                }
                else
                {
                    _receiver.Close();
                    _receiver.Dispose();
                    _receiver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    _receiver.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), _resolvedPort));
                    _receiver.ReceiveTimeout = TIMEOUT_MS;
                }
            }
            catch (Exception) { }
        }
    }

    public void Teardown()
    {
        _loop = false;
        _receiver.Close();
        _receiver.Dispose();
        _thread.Join();
    }
}
