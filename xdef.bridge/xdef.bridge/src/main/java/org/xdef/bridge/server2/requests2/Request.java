package org.xdef.bridge.server.requests;

import java.io.ByteArrayInputStream;
import java.io.DataOutputStream;
import java.io.IOException;

import org.xdef.bridge.utils.BinaryDataReader;
import org.xdef.bridge.utils.CustomDataInputStream;

public class Request {
    private int objectId;
    private int function;
    private int clientRequestId;
    private int serverRequestId;
    private byte[] data;

    public Request(final int function, final byte[] data) {
        this.function = function;
        this.data = data;
    }
    public Request(final int function, final byte[] data, final int objectId) {
        this(function, data);
        this.objectId = objectId;
    }

    public int getObjectId() {
        return objectId;
    }

    public void setObjectId(int objectId) {
        this.objectId = objectId;
    }

    public int getServerRequestId() {
        return serverRequestId;
    }

    public void setServerRequestId(int serverRequestId) {
        this.serverRequestId = serverRequestId;
    }

    public int getClientRequestId() {
        return clientRequestId;
    }

    public void setClientRequestId(int clientRequestId) {
        this.clientRequestId = clientRequestId;
    }

    public byte[] getData() {
        return data;
    }

    public void setData(final byte[] _data) {
        this.data = _data;
    }

    public int getFunction() {
        return function;
    }

    public void setFunction(final int _function) {
        this.function = _function;
    }

    public void writeToStream(DataOutputStream stream) throws IOException {
        stream.writeInt(objectId);
        stream.writeInt(function);
        stream.writeInt(clientRequestId);
        stream.writeInt(serverRequestId);
        stream.writeInt(data == null ? 0 : data.length);
        if (data != null)
            stream.write(data);
    }

    public static Request readFromStream(CustomDataInputStream stream) throws IOException {
        int objectId = stream.readInt();
        int function = stream.readInt();
        int clientReqId = stream.readInt();
        int serverReqId = stream.readInt();
        int payload = stream.readInt();
        byte[] data = stream.readNBytes(payload);
        Request req = new Request(function, data);
        req.setServerRequestId(serverReqId);
        req.setClientRequestId(clientReqId);
        req.setObjectId(objectId);
        return req;
    }

    public BinaryDataReader getReader() {
        BinaryDataReader reader =  new BinaryDataReader(new ByteArrayInputStream(data));
        return reader;
    }

}
