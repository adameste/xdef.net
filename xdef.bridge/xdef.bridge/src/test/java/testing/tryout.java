package testing;

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
        GenXComponent.genXComponent(pool, "D:/Source/xdef.net/xdef.bridge/xdef.bridge/src/test/java/xdefs/","utf8");
    }
}