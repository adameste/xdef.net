package org.xdef.bridge.wrappers.streams;

import java.io.IOException;
import java.io.OutputStream;

public class RemoteOutputStream extends OutputStream {
    private static final int DEFAULT_BUFFER_SIZE = 8196;
    protected byte[] buf;
    protected int count;
    private RemoteStreamWrapper streamWrapper;

    public RemoteOutputStream(RemoteStreamWrapper streamWrapper) {
        this(streamWrapper, DEFAULT_BUFFER_SIZE);
    }

    public RemoteOutputStream(RemoteStreamWrapper streamWrapper, int size) {
        super();
        buf = new byte[size];
    }

    public synchronized void flush() throws IOException {
        if (count == 0)
            return;

        streamWrapper.write(buf, 0, count);
        count = 0;
    }

    public synchronized void write(int b) throws IOException {
        if (count == buf.length)
            flush();

        buf[count] = (byte) (b & 0xFF);
        ++count;
    }

    public synchronized void write(byte[] buf, int offset, int len) throws IOException {
        // Buffer can hold everything. Note that the case where LEN < 0
        // is automatically handled by the downstream write.
        if (len < (this.buf.length - count)) {
            System.arraycopy(buf, offset, this.buf, count, len);
            count += len;
        } else {
            // The write was too big. So flush the buffer and write the new
            // bytes directly to the underlying stream, per the JDK 1.2
            // docs.
            flush();
            streamWrapper.write(buf, offset, len);
        }
    }
}