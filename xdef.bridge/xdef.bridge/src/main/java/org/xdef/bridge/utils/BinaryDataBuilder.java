package org.xdef.bridge.utils;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;

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

    public byte[] build() {
        try {
            dataStream.flush();
        } catch (IOException e) {
        }
        return stream.toByteArray();
    }


}