package org.xdef.bridge.server.requests;

import java.io.ByteArrayInputStream;
import java.io.DataOutputStream;
import java.io.IOException;

import org.xdef.bridge.utils.BinaryDataReader;
import org.xdef.bridge.utils.CustomDataInputStream;

public class Request {
    private int _objectId;
    private int _function;
    private int _clientRequestId;
    private int _serverRequestId;
    private byte[] data;

    public Request(final int function, final byte[] data) {
        _function = function;
        this.data = data;
    }

    public int getObjectId() {
        return _objectId;
    }

    public void setObjectId(int objectId) {
        this._objectId = objectId;
    }

    public int getServerRequestId() {
        return _serverRequestId;
    }

    public void setServerRequestId(int serverRequestId) {
        this._serverRequestId = serverRequestId;
    }

    public int getClientRequestId() {
        return _clientRequestId;
    }

    public void setClientRequestId(int clientRequestId) {
        this._clientRequestId = clientRequestId;
    }

    public byte[] getData() {
        return data;
    }

    public void setData(final byte[] _data) {
        this.data = _data;
    }

    public int getFunction() {
        return _function;
    }

    public void setFunction(final int _function) {
        this._function = _function;
    }

    public void writeToStream(DataOutputStream stream) throws IOException {
        stream.writeInt(_objectId);
        stream.writeInt(_function);
        stream.writeInt(_clientRequestId);
        stream.writeInt(_serverRequestId);
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
