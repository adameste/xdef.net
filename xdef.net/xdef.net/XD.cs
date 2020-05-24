using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using xdef.net.Connection;
using xdef.net.Sys;
using xdef.net.Utils;

namespace xdef.net
{
    public sealed class XD
    {
        private static Lazy<XD> _instance = new Lazy<XD>(() => new XD());
        private Process _xdefJavaProcess { get; set; }
        private Lazy<XDFactory> _xdFactory;
        private string _tmpFile;

        public static bool StartProcess { get; set; } = false;
        public static int Port { get; set; } = 42268;
        public static string Hostname { get; set; } = "localhost";
        public static string JavaExePath { get; set; }

        internal Client Client { get; private set; }


        private XD()
        {
            StartJavaBridge();
            StartClient();
            CreateXDFactoryInitializer();
        }

        private void StartClient()
        {
            Client = new ClientTcp(Port, Hostname);
            Client.Listen();
        }

        private void CreateXDFactoryInitializer()
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

            _tmpFile = Path.GetTempFileName();
            File.WriteAllBytes(_tmpFile, Properties.Resources.xdef_bridge);
            _xdefJavaProcess = Process.Start(new ProcessStartInfo()
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                FileName = JavaExePath ?? "java",
                Arguments = $"-jar \"{_tmpFile}\" {Port}"
            });
            var line = _xdefJavaProcess.StandardOutput.ReadLine(); // Listenin
            _xdefJavaProcess.OutputDataReceived += _xdefJavaProcess_OutputDataReceived;
            _xdefJavaProcess.BeginOutputReadLine();
            Debug.WriteLine($"Java process started: {line}");
        }

        private void _xdefJavaProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.Out.WriteLine(e.Data);
        }

        ~XD()
        {
            _xdefJavaProcess.OutputDataReceived -= _xdefJavaProcess_OutputDataReceived;
            Client?.Disconnect();
            if (_xdefJavaProcess?.HasExited == false)
            {
                _xdefJavaProcess.Kill();
            }
            try
            {
                File.Delete(_tmpFile);
            }
            catch (Exception) { } // Do nothing
        }

        internal static XD Instance => _instance.Value;
        public static XDFactory Factory => Instance._xdFactory.Value;

    }
}
