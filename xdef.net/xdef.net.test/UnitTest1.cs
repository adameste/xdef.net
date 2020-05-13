using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
            Parallel.For(1, 100, (i) =>
             {
                 for (int x = 0; x < 100; x++)
                 {
                     TestCreatePool();
                 }
             });
            return;
        }

        private void TestCreatePool()
        {
            var filePath = "xdefs/01.xdef";
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var pool = XD.Instance.Factory.CompileXD(null, stream);
            }
            return;
        }
    }
}
