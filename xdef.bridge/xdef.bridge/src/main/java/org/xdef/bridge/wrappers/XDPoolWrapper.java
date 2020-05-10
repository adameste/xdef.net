package org.xdef.bridge.wrappers;

import org.xdef.XDPool;
import org.xdef.bridge.remoteObjects.RemoteObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;

public class XDPoolWrapper extends RemoteObject {

    private XDPool xdPool;

    public XDPoolWrapper(Client client) {
        super(client);
    }

    public XDPoolWrapper(Client client, XDPool xdPool) {
        super(client);
        this.xdPool = xdPool;
    }

    @Override
    public Request handleRequest(Request request) {
        return null;
    }
    
}