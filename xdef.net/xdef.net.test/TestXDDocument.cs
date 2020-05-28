using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockHttpServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;
using xdef.net.Connection;
using xdef.net.Sys;
using xdef.net.Utils;

namespace xdef.net.test
{
    [TestClass]
    public class TestXDDocument
    {
        private XDPool _pool;
        private XDPool _poolCreate;
        private string _validFile;
        private XDDocument _doc;
        private XDDocument _docCreate;

        [TestInitialize]
        public void Initialize()
        {
            XD.StartProcess = TestConfig.StartProcess;
            _pool = XD.Factory.CompileXD(null, new FilePath("xdefs/02.xdef"));
            _poolCreate = XD.Factory.CompileXD(null, new FilePath("xdefs/03.xdef"));
            _validFile = "xdefs/02_medium.xml";
            _doc = _pool.CreateXDDocument();
            _docCreate = _poolCreate.CreateXDDocument();
        }


        [TestMethod]
        public void TestGetProps()
        {
            var reporter = new ArrayReporter();
            var props = _doc.GetProperties();
            Assert.IsNull(props);
        }

        [TestMethod]
        public void TestSetProps()
        {
            var doc = _pool.CreateXDDocument();
            doc.SetProperties(new Utils.Properties()
            {
                { "a", "b" },
                { "c", "d" }
            });
            var received = doc.GetProperties();
            Assert.AreEqual(received.Count, 2);
            Assert.AreEqual(received["a"], "b");
            Assert.AreEqual(received["c"], "d");
        }

        [TestMethod]
        public void TestParse()
        {
            var res = new List<XElement>();
            var reporter = new ArrayReporter();
            res.Add(_doc.Xparse(new FilePath(_validFile), reporter));
            res.Add(_doc.Xparse(File.ReadAllText(_validFile), reporter));
            using var stream = new FileStream(_validFile, FileMode.Open);
            res.Add(_doc.Xparse(stream, reporter));
            using (var mockServer = new MockServer(4444, "", (req, res, prm) => File.ReadAllText(_validFile)))
            {
                res.Add(_doc.Xparse(new Uri("http://localhost:4444"), reporter));
            }
            res.Add(_doc.Xparse(XDocument.Parse(File.ReadAllText(_validFile)).Root, reporter));
            using var stream2 = new FileStream(_validFile, FileMode.Open);
            res.Add(_doc.Xparse(stream2, null, reporter));
            res.Add(_doc.Xparse(File.ReadAllText(_validFile), null, reporter));
            Assert.IsTrue(res.All(p => res[0].ToString() == p.ToString()));
            Assert.IsFalse(reporter.Errors);
            Assert.AreEqual(reporter.ErrorCount, 0);
        }

        [TestMethod]
        public void TestInvalid()
        {
            var reporter = new ArrayReporter();
            Assert.ThrowsException<RemoteCallException>(() =>
            {
                _doc.Xparse(new FilePath("xdefs/02_invalid.xml"), null);
            });
            _doc.Xparse(new FilePath("xdefs/02_invalid.xml"), reporter);
            Assert.IsTrue(reporter.Errors);
            Assert.IsTrue(reporter.ErrorCount > 0);

        }

        [TestMethod]
        public void TestCreate()
        {
            _docCreate.SetXDContext(XDocument.Parse(File.ReadAllText("xdefs/03_valid.xml")).Root);
            var reporter = new ArrayReporter();
            var res = _docCreate.Xcreate("A", reporter);
        }

        [TestMethod]
        public void TestCreateInvalid()
        {
            var createInvalid = _poolCreate.CreateXDDocument();
            createInvalid.SetXDContext(File.ReadAllText("xdefs/03_invalid.xml"));
            var reporter = new ArrayReporter();
            var res = createInvalid.Xcreate("A", reporter);
            var a = reporter.Report;
            Assert.IsNotNull(res);
            Assert.IsNull(a);
        }

    }
}
