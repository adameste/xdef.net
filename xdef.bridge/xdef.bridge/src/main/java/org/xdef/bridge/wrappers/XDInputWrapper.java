package org.xdef.bridge.wrappers;

import org.xdef.XDInput;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;

public class XDInputWrapper extends RemoteHandlingObject {

    private XDInput xdInput;

    public XDInputWrapper(Client client, XDInput xdInput) {
        super(client);
        this.xdInput = xdInput;

        // TODO Auto-generated constructor stub
    }

    @Override
    public Response handleRequest(Request request) {
        // TODO Auto-generated method stub
        return null;
    }
    
}