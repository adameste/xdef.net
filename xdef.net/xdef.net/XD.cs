using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using xdef.net.Connection;
using xdef.net.Sys;
using xdef.net.Utils;

namespace xdef.net
{
    public class XD
    {
        private static Lazy<XD> _instance = new Lazy<XD>(() => new XD());
        private Process _xdefJavaProcess { get; set; }
        private Lazy<XDFactory> _xdFactory;

        public static bool StartProcess { get; set; } = false;

        public Client Client { get; private set; }


        private XD()
        {
            StartJavaBridge();
            Client = new ClientTcp();
            Client.Listen();
            CreateFactoryInitializer();
        }

        private void CreateFactoryInitializer()
        {
            _xdFactory = new Lazy<XDFactory>(() =>
            {
                var req = new CreateObjectRequest(CreateObjectRequest.OBJECT_XDFACTORY);
                var response = Client.SendRequestWithResponse(req);
                var objectId = BigEndianBitConverter.ToInt32(response.Data, 0);
                return new XDFactory(objectId, Client);
            });
        }

        private void StartJavaBridge()
        {
            if (!StartProcess) return;

            _xdefJavaProcess = Process.Start(new ProcessStartInfo()
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                FileName = "java",
                Arguments = "-jar JavaBin/xdef.bridge.jar"
            });
            var line = _xdefJavaProcess.StandardOutput.ReadLine(); // Listening
            Debug.WriteLine($"Java process started: {line}");
        }

        ~XD()
        {
            if (_xdefJavaProcess?.HasExited == false)
                _xdefJavaProcess.Kill();
            Client?.Disconnect();
        }

        public static XD Instance => _instance.Value;
        public XDFactory Factory => _xdFactory.Value;

    }
}
