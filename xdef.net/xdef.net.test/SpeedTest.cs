using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using xdef.net.Sys;
using xdef.net.Utils;

namespace xdef.net.test
{
    // [TestClass]
    // Dont run, slow
    public class SpeedTest
    {

        [TestInitialize]
        public void Init()
        {
            XD.StartProcess = false;
        }

        [TestMethod]
        public void TestSmallSpeed()
        {
            var pool = XD.Factory.CompileXD(null, new FilePath("xdefs/02.xdef"));
            RunXdef(pool, "xdefs/02_small.xml");
            double avg = 0;
            double min = double.MaxValue;
            double max = 0;
            for (int i = 0; i < 5; i++)
            {
                var start = DateTime.Now;
                for (int j = 0; j < 10000; j++)
                {
                    RunXdef(pool, "xdefs/02_small.xml");
                }
                var end = DateTime.Now;
                var time = (end - start).TotalSeconds;
                Trace.WriteLine($"{i}: " + time.ToString("0.00"));
                min = Math.Min(time, min);
                max = Math.Max(time, max);
                avg = ((avg * i) + time) / (i + 1);
            }
            Trace.WriteLine("avg: " + avg.ToString("0.00"));
            Trace.WriteLine("min: " + min.ToString("0.00"));
            Trace.WriteLine("max: " + max.ToString("0.00"));
        }

        [TestMethod]
        public void TestLargeSpeed()
        {
            var pool = XD.Factory.CompileXD(null, new FilePath("xdefs/02.xdef"));
            RunXdef(pool, "xdefs/02_large.xml");
            double avg = 0;
            double min = double.MaxValue;
            double max = 0;
            for (int i = 0; i < 5; i++)
            {
                var start = DateTime.Now;
                for (int j = 0; j < 20; j++)
                {
                    RunXdef(pool, "xdefs/02_large.xml");
                }
                var end = DateTime.Now;
                var time = (end - start).TotalSeconds;
                Trace.WriteLine($"{i}: " + time.ToString("0.00"));
                min = Math.Min(time, min);
                max = Math.Max(time, max);
                avg = ((avg * i) + time) / (i + 1);
            }
            Trace.WriteLine("avg: " + avg.ToString("0.00"));
            Trace.WriteLine("min: " + min.ToString("0.00"));
            Trace.WriteLine("max: " + max.ToString("0.00"));
        }

        [TestMethod]
        public void TestParallel()
        {
            var pool = XD.Factory.CompileXD(null, new FilePath("xdefs/02.xdef"));
            RunXdef(pool, "xdefs/02_medium.xml");
            double avg = 0;
            double min = double.MaxValue;
            double max = 0;
            for (int i = 0; i < 5; i++)
            {
                var start = DateTime.Now;
                Parallel.ForEach(Enumerable.Range(0, 10000), new ParallelOptions() { MaxDegreeOfParallelism = 24 },
                    (_) =>
                    {
                        RunXdef(pool, "xdefs/02_medium.xml");
                    });
                var end = DateTime.Now;
                var time = (end - start).TotalSeconds;
                Trace.WriteLine($"{i}: " + time.ToString("0.00"));
                min = Math.Min(time, min);
                max = Math.Max(time, max);
                avg = ((avg * i) + time) / (i + 1);
            }
            Trace.WriteLine("avg: " + avg.ToString("0.00"));
            Trace.WriteLine("min: " + min.ToString("0.00"));
            Trace.WriteLine("max: " + max.ToString("0.00"));
        }

        private void RunXdef(XDPool pool, string xmlFile)
        {
            var doc = pool.CreateXDDocument();
            var reporter = new ArrayReporter();
            using (var stream = new FileStream(xmlFile, FileMode.Open))
            {
                var res = doc.Xparse(stream, reporter);
            }
            try
            {
                if (reporter.Errors) throw new Exception("ValidationError");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(reporter);
                Trace.WriteLine(ex);
                throw;
            }


        }
    }
}
