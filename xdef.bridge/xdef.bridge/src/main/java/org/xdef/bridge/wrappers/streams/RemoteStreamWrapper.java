package org.xdef.bridge.wrappers.streams;

import java.io.IOException;

import org.xdef.bridge.remoteObjects.RemoteObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.utils.BinaryDataBuilder;
import org.xdef.bridge.utils.BinaryDataReader;

public class RemoteStreamWrapper extends RemoteObject {

    private static final int FUNCTION_AVAILABLE = 1;
    private static final int FUNCTION_CLOSE = 2;
    private static final int FUNCTION_READ = 3;
    private static final int FUNCTION_WRITE = 4;

    public RemoteStreamWrapper(Client client, int objectId) {
        super(client, objectId);
    }

    public int available() {
        Request response = sendRequestWithResponse(new Request(FUNCTION_AVAILABLE, null));
        try {
            return response.getReader().readInt();
        } catch (IOException e) {
            return 0;
        }
    }

    public void close() {
        sendRequest(new Request(FUNCTION_CLOSE, null));
    }

    public synchronized int read(byte[] buf, int offset, int len) {
        BinaryDataBuilder builder = new BinaryDataBuilder()
        .add(len);
        Request response = sendRequestWithResponse(new Request(FUNCTION_READ, builder.build()));
        try {
            BinaryDataReader reader = response.getReader();
            int payload = reader.readInt();
            byte[] data = reader.readNBytes(payload);
            System.arraycopy(data, 0, buf, offset, payload);
            return payload;
        } catch (IOException ex) {
            return 0;
        }
    }

    public synchronized void write(byte[] buf, int offset, int len) {
        BinaryDataBuilder builder = new BinaryDataBuilder()
                .add(len)
                .add(buf, offset, len);
        sendRequest(new Request(FUNCTION_WRITE, builder.build()));
    }

}