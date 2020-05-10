using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using xdef.net.Connection;
using xdef.net.Sys;
using xdef.net.Utils;

namespace xdef.net
{
    public class XDef
    {
        private static Lazy<XDef> _instance = new Lazy<XDef>();
        private static Process _xdefJavaProcess { get; set; }

        public static bool StartProcess = false;

        public Client Client { get; private set; }

        private XDef()
        {
            StartJavaBridge();
            Client = new TcpClient();
        }

        private static void StartJavaBridge()
        {
            if (!StartProcess) return;

            _xdefJavaProcess = Process.Start(new ProcessStartInfo()
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = "java",
                Arguments = "-jar xdef.bridge.jar"
            });
            _xdefJavaProcess.BeginOutputReadLine();
            var line = _xdefJavaProcess.StandardOutput.ReadLine(); // Listening
            Debug.WriteLine($"Java process started: {line}");
        }

        ~XDef()
        {
            if (_xdefJavaProcess?.HasExited == false)
                _xdefJavaProcess.Kill();
        }

        public static XDef Factory => _instance.Value;

        public XDPool CompileXD(Properties properties, params string[] sources)
        {
            throw new NotImplementedException();
        }

        public XDPool CompileXD(Properties properties, params FilePath[] sources)
        {
            throw new NotImplementedException();
        }

        public XDPool CompileXD(Properties properties, params Stream[] sources)
        {
            throw new NotImplementedException();
        }

        public XDPool CompileXD(Properties properties, params object[] sources)
        {
            throw new NotImplementedException();
        }

        public XDPool CompileXD(Properties properties, params Uri[] sources)
        {
            throw new NotImplementedException();
        }

        public XDPool CompileXD(ReportWriter reportWriter, Properties properties, params object[] sources)
        {
            throw new NotImplementedException();
        }
    }
}
