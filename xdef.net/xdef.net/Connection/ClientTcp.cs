using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace xdef.net.Connection
{
    public class ClientTcp : Client
    {
        private TcpClient _tcpClient;
        private bool _shouldListen = true;

        public ClientTcp()
        {
            _tcpClient = new TcpClient();
        }

        public override void Disconnect()
        {
            _shouldListen = false;
            _tcpClient.Close();
        }

        public override void Listen()
        {
            _tcpClient.Connect(IPAddress.Loopback, 42268);
            Task.Factory.StartNew(() =>
            {
                var stream = _tcpClient.GetStream();
                while (_shouldListen)
                {
                    try
                    {
                        var request = Request.ReadFromStream(stream);
                        HandleRequest(request);
                    } catch (Exception)
                    {
                        // Disconnected, ignore this exception
                    }
                }
            });
        }

        protected override void SendRequestData(Request request)
        {
            var stream = _tcpClient.GetStream();
            request.WriteToStream(stream);
            stream.Flush();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tcpClient?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
