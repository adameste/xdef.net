package testing;

import java.io.File;
import java.util.Properties;


import org.junit.Test;
import org.xdef.XDBuilder;
import org.xdef.XDFactory;
import org.xdef.XDPool;

public class tryout {

    @Test
    public void testXdef() throws Exception {
        File file = new File("D:\\Source\\xdef.net\\xdef.net\\xdef.net.test\\xdefs\\01.xdef");
        XDPool pool = XDFactory.compileXD(null, file);
        return;
    }
}