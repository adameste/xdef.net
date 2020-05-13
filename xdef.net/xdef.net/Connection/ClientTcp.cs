using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace xdef.net.Connection
{
    public class ClientTcp : Client
    {
        private TcpClient _tcpClient;
        private bool _shouldListen = true;
        private object _sendLock = new object();

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
            _tcpClient.Connect("127.0.0.1", 42268);
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
    }
}
