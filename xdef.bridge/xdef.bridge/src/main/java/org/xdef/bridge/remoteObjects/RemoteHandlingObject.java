package org.xdef.bridge.remoteObjects;

import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;

public abstract class RemoteHandlingObject extends RemoteObject {

    public RemoteHandlingObject(Client client) {
        super(client, 0);
    }

    public abstract Response handleRequest(Request request);
    
    @Override
    protected void deleteRemoteObject() {
        // Do nothing
    }
}