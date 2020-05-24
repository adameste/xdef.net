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
        buf = new byte[size];
    }

    @Override
    public synchronized void flush() throws IOException {
        if (count == 0)
            return;

        streamWrapper.write(buf, 0, count);
        count = 0;
    }

    @Override
    public synchronized void write(int b) throws IOException {
        if (count == buf.length)
            flush();

        buf[count] = (byte) (b & 0xFF);
        ++count;
    }

    @Override
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

    @Override
    public synchronized void write(byte[] b) throws IOException {
        write(b, 0, b.length);
    }

    @Override
    protected void finalize() throws Throwable {
        try {
            flush();
        } catch (Exception ex) {
        } // Network error, can't throw exception in finalizer
        super.finalize();
    }
}