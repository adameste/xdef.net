package org.xdef.bridge.wrappers;

import org.xdef.XDBuilder;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;

public class XDBuilderWrapper extends RemoteHandlingObject {

    private XDBuilder builder;

    public XDBuilderWrapper(Client client, XDBuilder builder) {
        super(client);
        this.builder = builder;
        
    }

    @Override
    public Response handleRequest(Request request) {
        return null;
    }
    
}