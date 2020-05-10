package org.xdef.bridge.remoteObjects;

import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;

public abstract class RemoteObject {

    private Client client;
    private int objectId;

    public RemoteObject(Client client) {
        this.client = client;
    }

    public int getObjectId() {
        return objectId;
    }

    public void setObjectId(int objectId) {
        this.objectId = objectId;
    }

    public abstract Request handleRequest(Request request);

    protected void sendRequest(Request request) {
        request.setObjectId(objectId);
        client.sendPacketWithoutResponse(request);
    }

    protected Request sendRequestWithResponse(Request request) {
        request.setObjectId(objectId);
        return client.sendPacketWithResponse(request);
    }
}