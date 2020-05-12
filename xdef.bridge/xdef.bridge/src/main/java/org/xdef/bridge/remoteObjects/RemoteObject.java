package org.xdef.bridge.remoteObjects;

import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;

public abstract class RemoteObject {

    protected Client client;
    private int objectId;

    public RemoteObject(Client client, int objectId) {
        this.client = client;
        this.objectId = objectId;
    }

    public int getObjectId() {
        return objectId;
    }

    public void setObjectId(int objectId) {
        this.objectId = objectId;
    }

    protected void sendRequest(Request request) {
        request.setObjectId(objectId);
        client.sendRequestWithoutResponse(request);
    }

    protected Request sendRequestWithResponse(Request request) {
        request.setObjectId(objectId);
        return client.sendRequestWithResponse(request);
    }

    @Override
    protected void finalize() throws Throwable {
        deleteRemoteObject();
        super.finalize();
    }

    protected void deleteRemoteObject(){
        client.deleteRemoteObject(this);
    }

}