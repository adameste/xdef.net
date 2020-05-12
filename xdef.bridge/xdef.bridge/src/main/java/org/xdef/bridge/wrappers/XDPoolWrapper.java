package org.xdef.bridge.wrappers;

import org.xdef.XDPool;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;

public class XDPoolWrapper extends RemoteHandlingObject {

    private XDPool xdPool;

    public XDPoolWrapper(Client client) {
        super(client);
    }

    public XDPoolWrapper(Client client, XDPool xdPool) {
        super(client);
        this.xdPool = xdPool;
    }

    @Override
    public Response handleRequest(Request request) {
        return null;
    }
    
}