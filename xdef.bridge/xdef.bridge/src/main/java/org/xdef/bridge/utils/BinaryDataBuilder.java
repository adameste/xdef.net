package org.xdef.bridge.utils;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.util.logging.Level;
import java.util.logging.Logger;

public class BinaryDataBuilder {
    private ByteArrayOutputStream stream;
    private DataOutputStream dataStream;

    public BinaryDataBuilder() {
        stream = new ByteArrayOutputStream();
        dataStream = new DataOutputStream(stream);
    }

    public BinaryDataBuilder add(int x) {
        try {
            dataStream.writeInt(x);
        } catch (IOException e) {
        }
        return this;
    }

    public BinaryDataBuilder add(String x) {
        try {
            byte[] data = x.getBytes(StandardCharsets.UTF_8);
            dataStream.writeInt(data.length);
            dataStream.write(data);
        } catch (IOException e) {
        }
        return this;
    }
    
    public BinaryDataBuilder add(byte[] buf, int offset, int len) {
        try {
            dataStream.write(buf, offset, len);
        } catch (IOException ex) {
            // Do nothing
        }
        return this;
    }
    
     public BinaryDataBuilder add(boolean x) {
        try {
            dataStream.writeBoolean(x);
        } catch (IOException ex) {
            // Do nothing
        }
        return this;
    }

    public byte[] build() {
        try {
            dataStream.flush();
        } catch (IOException e) {
        }
        return stream.toByteArray();
    }


}