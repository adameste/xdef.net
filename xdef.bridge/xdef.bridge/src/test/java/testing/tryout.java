package testing;

import java.io.Console;
import java.io.File;
import java.io.FileInputStream;

import org.junit.Test;
import org.w3c.dom.Element;
import org.xdef.XDDocument;
import org.xdef.XDFactory;
import org.xdef.XDPool;
import org.xdef.component.GenXComponent;
import org.xdef.sys.ArrayReporter;

public class tryout {

    @Test
    public void testXdef() throws Exception {

        File f = new File("D:/Source/xdef.net/xdef.bridge/xdef.bridge/src/test/java/xdefs/01.xdef");
        boolean a = f.exists();
        XDPool pool = XDFactory.compileXD(null, f);
        XDDocument doc = pool.createXDDocument();
        GenXComponent.genXComponent(pool, "D:/Source/xdef.net/xdef.bridge/xdef.bridge/src/test/java/xdefs/", "utf8");
    }

    @Test
    public void testSmallSpeed() throws Exception
        {
            XDPool pool = XDFactory.compileXD(null, new File("D:\\Source\\xdef.net\\xdef.net\\xdef.net.test\\xdefs\\02.xdef"));
            runXdef(pool, "D:\\Source\\xdef.net\\xdef.net\\xdef.net.test\\xdefs\\02_small.xml");
            double avg = 0;
            double min = Double.MAX_VALUE;
            double max = 0;
            for (int i = 0; i < 5; i++)
            {
                long start = System.nanoTime();
                for (int j = 0; j < 10; j++)
                {
                    runXdef(pool, "D:\\Source\\xdef.net\\xdef.net\\xdef.net.test\\xdefs\\02_small.xml");
                }
                long end = System.nanoTime();
                double time = (end - start) / 1000000000.0;
                System.out.println(i +": " + Double.toString(time));;
                min = Double.min(time, min);
                max = Double.max(time, max);
                avg = ((avg * i) + time) / (i + 1);
            }
            System.out.println("avg: " + Double.toString(avg));
            System.out.println("min: " + Double.toString(min));
            System.out.println("max: " + Double.toString(max));
        }

        @Test
        public void testLargeSpeed() throws Exception
            {
                XDPool pool = XDFactory.compileXD(null, new File("D:\\Source\\xdef.net\\xdef.net\\xdef.net.test\\xdefs\\02.xdef"));
                runXdef(pool, "D:\\Source\\xdef.net\\xdef.net\\xdef.net.test\\xdefs\\02_large.xml");
                double avg = 0;
                double min = Double.MAX_VALUE;
                double max = 0;
                for (int i = 0; i < 5; i++)
                {
                    long start = System.nanoTime();
                    for (int j = 0; j < 300; j++)
                    {
                        runXdef(pool, "D:\\Source\\xdef.net\\xdef.net\\xdef.net.test\\xdefs\\02_large.xml");
                    }
                    long end = System.nanoTime();
                    double time = (end - start) / 1000000000.0;
                    System.out.println(i +": " + Double.toString(time));;
                    min = Double.min(time, min);
                    max = Double.max(time, max);
                    avg = ((avg * i) + time) / (i + 1);
                }
                System.out.println("avg: " + Double.toString(avg));
                System.out.println("min: " + Double.toString(min));
                System.out.println("max: " + Double.toString(max));
            }

    private void runXdef(XDPool pool, String xmlFile) throws Exception {
        XDDocument doc = pool.createXDDocument();
        ArrayReporter reporter = new ArrayReporter();
        Element res = doc.xparse(new File(xmlFile), reporter);
        if (reporter.errors())
            throw new Exception("ValidationError");
    }
}