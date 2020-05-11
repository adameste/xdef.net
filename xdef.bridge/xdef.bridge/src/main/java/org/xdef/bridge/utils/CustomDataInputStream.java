package org.xdef.bridge.utils;

import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Objects;

public class CustomDataInputStream extends DataInputStream {

    public CustomDataInputStream(InputStream in) {
        super(in);
    }

    public int readNBytes(byte[] b, int off, int len) throws IOException {
        Objects.requireNonNull(b);
        if (off < 0 || len < 0 || len > b.length - off)
            throw new IndexOutOfBoundsException();
        int n = 0;
        while (n < len) {
            int count = read(b, off + n, len - n);
            if (count < 0)
                break;
            n += count;
        }
        return n;
    }

    public byte[] readNBytes(int len) throws IOException{
        if (len < 0) throw new IndexOutOfBoundsException();
        byte[] n = new byte[len];
        readNBytes(n, 0, len);
        return n;
    }

    
}