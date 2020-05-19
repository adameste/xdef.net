package testing;

import java.io.File;
import java.io.FileInputStream;

import org.junit.Test;
import org.w3c.dom.Element;
import org.xdef.XDDocument;
import org.xdef.XDFactory;
import org.xdef.XDPool;
import org.xdef.sys.ArrayReporter;

public class tryout {

    @Test
    public void testXdef() throws Exception {
        XDFactory.xparse(new FileInputStream("D:/Source/xdef.net/xdef.net/xdef.net.test/xdefs/01.xdef"), null);
    }
}