package testing;

import java.io.File;
import java.util.Properties;


import org.junit.Test;
import org.xdef.XDBuilder;
import org.xdef.XDFactory;
import org.xdef.XDPool;
import org.xdef.msg.JSON;

public class tryout {

    @Test
    public void testXdef() throws Exception {
        File file = new File("src/test/java/xdefs/01.xdef");
        Properties props = new Properties();
        XDPool pool = XDFactory.compileXD(props, file);
        XDBuilder builder = XDFactory.getXDBuilder(null);
        return;
    }
}