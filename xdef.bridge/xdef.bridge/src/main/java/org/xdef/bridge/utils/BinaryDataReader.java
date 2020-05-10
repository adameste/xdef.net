package org.xdef.bridge.utils;

import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;

public class BinaryDataReader extends DataInputStream {

    public BinaryDataReader(InputStream in) {
        super(in);
    }

    public String readSharpString() throws IOException {
        var payload = readInt();
        if (payload == 0) return null;
        var data = readNBytes(payload);
        return new String(data, StandardCharsets.UTF_8);
    }
    
}