using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using xdef.net.Utils;

namespace xdef.net.test
{
    [TestClass]
    public class TestXDBuilder
    {
        private MockServer _mockServer;
        private string _filePath1;
        private string _filePath2;

        [TestInitialize]
        public void Initialize()
        {
            XD.StartProcess = TestConfig.StartProcess;
            _mockServer = new MockServer(3334, "", (req, res, prm) => File.ReadAllText("xdefs/02.xdef"));
            _filePath1 = "xdefs/01.xdef";
            _filePath2 = "xdefs/02.xdef";
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockServer.Dispose();
        }

        [TestMethod]
        public void TestFile()
        {
            var builder = XD.Factory.GetXDBuilder(null);
            builder.SetSource(new FilePath(_filePath1));
            var pool = builder.CompileXD();
            Assert.IsNotNull(pool);
        }

        [TestMethod]
        public void TestString()
        {
            var builder = XD.Factory.GetXDBuilder(null);
            builder.SetSource(File.ReadAllText(_filePath1));
            builder.SetSource(File.ReadAllText(_filePath2), null);
            var pool = builder.CompileXD();
            Assert.IsNotNull(pool);
        }

        [TestMethod]
        public void TestUrl()
        {
            var builder = XD.Factory.GetXDBuilder(null);
            builder.SetSource(new Uri("http://localhost:3334"));
            var pool = builder.CompileXD();
            Assert.IsNotNull(pool);
        }

        [TestMethod]
        public void TestStream()
        {
            var builder = XD.Factory.GetXDBuilder(null);
            using var stream = new FileStream(_filePath1, FileMode.Open);
            builder.SetSource(stream, null);
            var pool = builder.CompileXD();
            Assert.IsNotNull(pool);
        }

        [TestMethod]
        public void TestStreams()
        {
            var builder = XD.Factory.GetXDBuilder(null);
            using var s1 = new FileStream(_filePath1, FileMode.Open);
            using var s2 = new FileStream(_filePath2, FileMode.Open);
            builder.SetSource(new Stream[] { s1 }, new string[] { null });
            var pool = builder.CompileXD();
            Assert.IsNotNull(pool);
        }

        [TestMethod]
        public void TestStrings()
        {
            var builder = XD.Factory.GetXDBuilder(null);
            builder.SetSource(new string[] { File.ReadAllText(_filePath1) }, new string[] { null });
            var pool = builder.CompileXD();
            Assert.IsNotNull(pool);
        }
    }
}
