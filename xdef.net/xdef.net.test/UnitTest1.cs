using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Enumeration;
using System.Threading.Tasks;
using System.Xml.Linq;
using xdef.net.Sys;
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
            var pool = XD.Factory.CompileXD(null,new FilePath("xdefs/02.xdef"));
            var doc = pool.CreateXDDocument();
            var reporter = new ArrayReporter();
            var res = doc.XParse(new FilePath("xdefs/02.xml"), reporter);
            var aa = reporter.PrintToString();
        }

    }
}
