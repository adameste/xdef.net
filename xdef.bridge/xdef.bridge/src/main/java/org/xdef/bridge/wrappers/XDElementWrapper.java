package org.xdef.bridge.wrappers;

import org.xdef.XDElement;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;

public class XDElementWrapper extends RemoteHandlingObject {

    private XDElement xdElement;

    public XDElementWrapper(Client client, XDElement xdElement) {
        super(client);
        // TODO Auto-generated constructor stub
        this.xdElement = xdElement;
    }

    public XDElement getXdElement() {
        return xdElement;
    }

    @Override
    public Response handleRequest(Request request) {
        // TODO Auto-generated method stub
        return null;
    }
    
}