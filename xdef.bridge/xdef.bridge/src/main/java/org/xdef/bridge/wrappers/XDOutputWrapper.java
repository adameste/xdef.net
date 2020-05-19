package org.xdef.bridge.wrappers;

import org.xdef.XDOutput;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;

public class XDOutputWrapper extends RemoteHandlingObject {

    private XDOutput xdOutput;

    public XDOutputWrapper(final Client client, XDOutput xdOutput) {
        super(client);
        this.xdOutput = xdOutput;
    }

    @Override
    public Response handleRequest(Request request) {
        // TODO Auto-generated method stub
        return null;
    }
    
}