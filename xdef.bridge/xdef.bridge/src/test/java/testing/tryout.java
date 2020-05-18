package testing;

import java.io.File;

import org.junit.Test;
import org.w3c.dom.Element;
import org.xdef.XDDocument;
import org.xdef.XDFactory;
import org.xdef.XDPool;
import org.xdef.sys.ArrayReporter;

public class tryout {

    @Test
    public void testXdef() throws Exception {
        XDPool pool = XDFactory.compileXD(null, new File("D:/Source/xdef.net/xdef.net/xdef.net.test/xdefs/02.xdef"));
            for (int i = 0; i< 10000; i++)
            {

                 XDDocument doc = pool.createXDDocument();
                 ArrayReporter reporter = new ArrayReporter();
                 Element res = doc.xparse(new File("D:/Source/xdef.net/xdef.net/xdef.net.test/xdefs/02.xml"), reporter);
                 String aa = reporter.printToString();
            }
    }
}