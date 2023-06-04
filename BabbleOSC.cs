﻿using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using VRCFaceTracking.Core.OSC;

namespace VRCFaceTracking.Babble;
public partial class BabbleOSC
{
    private Socket _receiver;
    private readonly Thread _thread;
    private readonly ILogger _logger;
    private readonly int _resolvedPort;
    private const int DEFAULT_PORT = 8888;
    private const int TIMEOUT_MS = 10_000;

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
        _receiver.ReceiveTimeout = TIMEOUT_MS;

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
                if (_receiver.Connected)
                {
                    var length = _receiver.Receive(buffer);
                    if (SROSCLib.parse_osc(buffer, length, ref oscMeta))
                    {
                        if (BabbleExpressionMap.ContainsKey2(oscMeta.Address))
                        {
                            if (oscMeta.Type == OscValueType.Float) // Possibly redundant
                            {
                                BabbleExpressionMap.SetByKey2(oscMeta.Address, oscMeta.Value.FloatValues[0]); 
                            }
                        }
                    }
                }
                else
                {
                    _receiver.Close();
                    _receiver.Dispose();
                    _receiver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    _receiver.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), _resolvedPort));
                    _receiver.ReceiveTimeout = TIMEOUT_MS;
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
