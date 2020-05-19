package org.xdef.bridge.wrappers;

import org.xdef.XDService;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;

public class XDServiceWrapper extends RemoteHandlingObject {

    private XDService xdService;

    public XDServiceWrapper(Client client, XDService xdService) {
        super(client);
        this.xdService = xdService;
    }

    @Override
    public Response handleRequest(Request request) {
        // TODO Auto-generated method stub
        return null;
    }
    
}