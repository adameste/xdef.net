using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

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
                for (int x = 0; x < 1000; x++)
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
