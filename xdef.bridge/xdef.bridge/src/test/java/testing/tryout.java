package testing;

import java.io.File;
import java.util.Properties;

import com.fasterxml.jackson.databind.ObjectMapper;

import org.junit.Test;
import org.xdef.XDFactory;
import org.xdef.msg.JSON;

public class tryout {



    @Test
    public void testXdef() throws Exception{
        var file = new File("src/test/java/xdefs/01.xdef");
        var props = new Properties();
        var pool = XDFactory.compileXD(props, file);
        var table = pool.getVariableTable();
        var document = pool.createXDDocument();
        var mapper = new ObjectMapper();
        var str = mapper.writeValueAsString(table);
        return;
    }
}