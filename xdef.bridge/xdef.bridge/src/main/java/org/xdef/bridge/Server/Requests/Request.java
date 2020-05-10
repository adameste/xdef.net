package org.xdef.bridge.server.requests;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;

public class Request {
    private int _objectId;
    private int _function;
    private int _clientRequestId;
    private int _serverRequestId;
    private byte[] _data;

    public Request(final int function, final byte[] data) {
        _function = function;
        _data = data;
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
        return _data;
    }

    public void setData(final byte[] _data) {
        this._data = _data;
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
        stream.writeInt(_data == null ? 0 : _data.length);
        if (_data != null)
            stream.write(_data);
    }

    public static Request readFromStream(DataInputStream stream) throws IOException {
        var objectId = stream.readInt();
        var function = stream.readInt();
        var clientReqId = stream.readInt();
        var serverReqId = stream.readInt();
        var payload = stream.readInt();
        var data = stream.readNBytes(payload);
        var req = new Request(function, data);
        req.setServerRequestId(serverReqId);
        req.setClientRequestId(clientReqId);
        req.setObjectId(objectId);
        return req;
    }

}
