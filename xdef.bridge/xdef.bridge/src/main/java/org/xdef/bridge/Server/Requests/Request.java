package org.xdef.bridge.Server.Requests;

import java.io.DataOutputStream;
import java.io.IOException;

public class Request implements StreamWritable {
    private Function _function;
    private int _requestId;
    private byte[] _data;

    public Request(final Function function, final byte[] data) {
        _function = function;
        _data = data;
    }

    public int getRequestId() {
        return _requestId;
    }

    public void setRequestId(int _requestId) {
        this._requestId = _requestId;
    }

    public byte[] getData() {
        return _data;
    }

    public void setData(final byte[] _data) {
        this._data = _data;
    }

    public Function getFunction() {
        return _function;
    }

    public void setFunction(final Function _function) {
        this._function = _function;
    }

    @Override
    public void writeToStream(DataOutputStream stream) throws IOException {
        stream.writeInt(8 + _data.length);
        stream.writeInt(_function.ordinal());
        stream.writeInt(_requestId);
        stream.write(_data);
    }
}
