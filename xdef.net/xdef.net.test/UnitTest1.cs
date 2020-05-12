using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using xdef.net.Utils;

namespace xdef.net.test
{
    [TestClass]
    public class UnitTest1
    {

        [TestInitialize]
        public void Init()
        {
            XD.StartProcess = false;
        }

        [TestMethod]
        public void TestMethod1()
        {
            for (int i = 0; i < 5; i++)
            {
                TestCreatePool();
                GC.Collect(0);
            }
            return;
        }

        private void TestCreatePool()
        {
            var filePath = "xdefs/01.xdef";
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var pool = XD.Instance.Factory.CompileXD(null, stream);
            }
        }
    }
}
