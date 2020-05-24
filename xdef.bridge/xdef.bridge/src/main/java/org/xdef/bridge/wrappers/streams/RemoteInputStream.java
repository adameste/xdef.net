/*
 * Copyright (c) 1994, 2013, Oracle and/or its affiliates. All rights reserved.
 * DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
 *
 * This code is free software; you can redistribute it and/or modify it
 * under the terms of the GNU General Public License version 2 only, as
 * published by the Free Software Foundation.  Oracle designates this
 * particular file as subject to the "Classpath" exception as provided
 * by Oracle in the LICENSE file that accompanied this code.
 *
 * This code is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License
 * version 2 for more details (a copy is included in the LICENSE file that
 * accompanied this code).
 *
 * You should have received a copy of the GNU General Public License version
 * 2 along with this work; if not, write to the Free Software Foundation,
 * Inc., 51 Franklin St, Fifth Floor, Boston, MA 02110-1301 USA.
 *
 * Please contact Oracle, 500 Oracle Parkway, Redwood Shores, CA 94065 USA
 * or visit www.oracle.com if you need additional information or have any
 * questions.
 */

package org.xdef.bridge.wrappers.streams;

import java.io.IOException;
import java.io.InputStream;

public class RemoteInputStream extends InputStream {

    private RemoteStreamWrapper streamWrapper;
    private static final int DEFAULT_BUFFER_SIZE = 16384;
    protected byte[] buf;
    protected int count;
    protected int pos;
    protected int markpos = -1;
    protected int marklimit;
    private final int bufferSize;

    public RemoteInputStream(RemoteStreamWrapper streamWrapper) {
        this(streamWrapper, DEFAULT_BUFFER_SIZE);
    }

    public RemoteInputStream(RemoteStreamWrapper streamWrapper, int size) {
        this.streamWrapper = streamWrapper;
        if (size <= 0)
            throw new IllegalArgumentException();
        buf = new byte[size];
        // initialize pos & count to bufferSize, to prevent refill from
        // allocating a new buffer (if the caller starts out by calling mark()).
        pos = count = bufferSize = size;
    }

    @Override
    public synchronized int available() throws IOException {
        return count - pos + streamWrapper.available();
    }

    @Override
    public void close() throws IOException {
        buf = null;
        pos = count = 0;
        markpos = -1;
        streamWrapper.close();
    }

    @Override
    public synchronized void mark(int readlimit) {
        marklimit = readlimit;
        markpos = pos;
    }

    @Override
    public boolean markSupported() {
        return true;
    }

    @Override
    public synchronized int read() throws IOException {
        if (pos >= count && !refill())
            return -1; // EOF

        return buf[pos++] & 0xFF;
    }

    @Override
    public synchronized int read(byte[] b, int off, int len) throws IOException {
        if (off < 0 || len < 0 || b.length - off < len)
            throw new IndexOutOfBoundsException();

        if (len == 0)
            return 0;

        if (pos >= count && !refill())
            return -1; // No bytes were read before EOF.

        int totalBytesRead = Math.min(count - pos, len);
        System.arraycopy(buf, pos, b, off, totalBytesRead);
        pos += totalBytesRead;
        off += totalBytesRead;
        len -= totalBytesRead;

        while (len > 0 && streamWrapper.available() > 0 && refill()) {
            int remain = Math.min(count - pos, len);
            System.arraycopy(buf, pos, b, off, remain);
            pos += remain;
            off += remain;
            len -= remain;
            totalBytesRead += remain;
        }

        return totalBytesRead;
    }

    @Override
    public synchronized void reset() throws IOException {
        if (markpos == -1)
            throw new IOException(buf == null ? "Stream closed." : "Invalid mark.");

        pos = markpos;
    }

    @Override
    public synchronized long skip(long n) throws IOException {
        if (buf == null)
            throw new IOException("Stream closed.");

        final long origN = n;

        while (n > 0L) {
            if (pos >= count && !refill())
                break;

            int numread = (int) Math.min((long) (count - pos), n);
            pos += numread;
            n -= numread;
        }

        return origN - n;
    }

    private boolean refill() throws IOException {
        if (buf == null)
            throw new IOException("Stream closed.");

        if (markpos == -1 || count - markpos >= marklimit) {
            markpos = -1;
            pos = count = 0;
        } else {
            byte[] newbuf = buf;
            if (markpos < bufferSize) {
                newbuf = new byte[count - markpos + bufferSize];
            }
            System.arraycopy(buf, markpos, newbuf, 0, count - markpos);
            buf = newbuf;
            count -= markpos;
            pos -= markpos;
            markpos = 0;
        }

        int numread = streamWrapper.read(buf, count, bufferSize);

        if (numread <= 0) // EOF
            return false;

        count += numread;
        return true;
    }
}