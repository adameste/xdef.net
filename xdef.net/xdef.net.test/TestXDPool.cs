using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Utils;

namespace xdef.net.test
{
    [TestClass]
    public class TestXDPool
    {
        private XDPool _pool1;
        private XDPool _pool2;

        [TestInitialize]
        public void Initialize()
        {
            XD.StartProcess = TestConfig.StartProcess;
            _pool1 = XD.Factory.CompileXD(null, new FilePath("xdefs/01.xdef"));
            _pool2 = XD.Factory.CompileXD(null, new FilePath("xdefs/01.xdef"), new FilePath("xdefs/02.xdef"));
        }

        [TestMethod]
        public void TestGetVersioninfo()
        {
            var str = _pool1.GetVersionInfo();
            Assert.IsNotNull(str);
        }

        [TestMethod]
        public void TestCreateXDDocument()
        {
            var d1 = _pool1.CreateXDDocument();
            var d2 = _pool2.CreateXDDocument("FO");
            var d3 = _pool2.CreateXDDocument("W");
            Assert.IsNotNull(d1);
            Assert.IsNotNull(d2);
            Assert.IsNotNull(d3);
        }
        [TestMethod]
        public void TestMinMax()
        {
            Assert.AreEqual(_pool1.GetMinYear(), int.MinValue);
            Assert.AreEqual(_pool1.GetMaxYear(), int.MinValue);
        }

    }
}
