using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using xdef.net.Sys;
using xdef.net.Utils;

namespace xdef.net.test
{
    [TestClass]
    public class TestXDFactory
    {
        private MockServer _mockServer;

        [TestInitialize]
        public void Initialize()
        {
            XD.StartProcess = TestConfig.StartProcess;
            _mockServer = new MockServer(3333, "", (req, res, prm) => File.ReadAllText("xdefs/02.xdef"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockServer.Dispose();
        }

        public void x()
        {
            var pool = XD.Factory.CompileXD(null, new FilePath("x-definition file path"));
            var doc = pool.CreateXDDocument();
            var arrayReporter = new ArrayReporter();
            doc.Xparse(new FilePath("xml file path"), arrayReporter);
            if (arrayReporter.Errors)
            {
                // Chyba
            }
        }
        [TestMethod]
        public void TestCompile()
        {
            List<XDPool> pools = new List<XDPool>();
            pools.Add(XD.Factory.CompileXD(null, new FilePath("xdefs/02.xdef")));
            pools.Add(XD.Factory.CompileXD(null, File.ReadAllText("xdefs/02.xdef")));
            pools.Add(XD.Factory.CompileXD(null, new FileStream("xdefs/02.xdef", FileMode.Open)));
            pools.Add(XD.Factory.CompileXD(null, "http://localhost:3333"));
            pools.Add(XD.Factory.CompileXD(null, new FilePath("xdefs/01.xdef"), File.ReadAllText("xdefs/02.xdef")));
            /*pools.Add(XD.Factory.CompileXD(null,
                new object[] { new FilePath("xdefs/01.xdef"), File.ReadAllText("xdefs/02.xdef") },
                new string[] { "FO", "W" }));*/ // TODO
            pools.Add(XD.Factory.CompileXD(new ArrayReporter(), null,
                new FilePath("xdefs/01.xdef"), File.ReadAllText("xdefs/02.xdef")));
            Assert.AreEqual(pools.Where(p => p != null).Count(), 6);
        }

        [TestMethod]
        public async Task TestGetBuilder()
        {
            var reporter = new ArrayReporter();
            var b1 = XD.Factory.GetXDBuilder(reporter, null);
            var b2 = XD.Factory.GetXDBuilder(null);
            Assert.IsNotNull(b1);
            Assert.IsNotNull(b2);
        }
    }
}
