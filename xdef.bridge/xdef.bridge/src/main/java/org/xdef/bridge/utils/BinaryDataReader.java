package org.xdef.bridge.utils;
import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;

public class BinaryDataReader extends CustomDataInputStream {

    public BinaryDataReader(InputStream in) {
        super(in);
    }

    public String readSharpString() throws IOException {
        int payload = readInt();
        if (payload == 0) return null;
        byte[] data = readNBytes(payload);
        return new String(data, StandardCharsets.UTF_8);
    }
    
}