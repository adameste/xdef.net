using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace xdef.net.Connection
{
    internal class ClientTcp : Client
    {
        private TcpClient _tcpClient;
        private bool _shouldListen = true;
        private object _sendLock = new object();
        private readonly int _port;
        private readonly string _hostname;
        private bool _isLocalhost = false;
        internal override bool IsLocalhost => _isLocalhost;

       

        public ClientTcp(int port, string hostname)
        {
            _tcpClient = new TcpClient();
            _port = port;
            _hostname = hostname;
            _isLocalhost = IsLocalIpAddress(hostname);
        }

        public override void Disconnect()
        {
            _shouldListen = false;
            _tcpClient.Close();
        }

        public override void Listen()
        {
            _tcpClient.Connect(_hostname, _port);
            var thread = new Thread(() =>
            {
                var stream = _tcpClient.GetStream();
                while (_shouldListen)
                {
                    try
                    {
                        var request = Request.ReadFromStream(stream);
                        HandleRequest(request);
                    }
                    catch (Exception)
                    {
                        // Disconnected, ignore this exception
                    }
                }
            });
            thread.Priority = ThreadPriority.AboveNormal;
            thread.IsBackground = true;
            thread.Start();
        }

        protected override void SendRequestData(Request request)
        {
            var stream = _tcpClient.GetStream();
            lock (_sendLock)
            {
                request.WriteToStream(stream);
                stream.Flush();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tcpClient?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override async Task SendRequestDataAsync(Request request)
        {
            var stream = _tcpClient.GetStream();
            await request.WriteToStreamAsync(stream);
            await stream.FlushAsync();
        }

        private static bool IsLocalIpAddress(string host)
        {
            try
            { // get host IP addresses
                IPAddress[] hostIPs = Dns.GetHostAddresses(host);
                // get local IP addresses
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // test if any host IP equals to any local IP or to localhost
                foreach (IPAddress hostIP in hostIPs)
                {
                    // is localhost
                    if (IPAddress.IsLoopback(hostIP)) return true;
                    // is local address
                    foreach (IPAddress localIP in localIPs)
                    {
                        if (hostIP.Equals(localIP)) return true;
                    }
                }
            }
            catch { }
            return false;
        }
    }
}
