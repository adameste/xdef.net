package org.xdef.bridge.remoteObjects;

import org.xdef.XDFactory;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;

public class XDFactoryWrapper extends RemoteObject {

    private static final int FUNCTION_COMPILE_XD_1 = 1;

    public XDFactoryWrapper(Client client) {
        super(client);
    }

    @Override
    public Request handleRequest(Request request) {
        switch(request.getFunction())
        {
            default:
            return null;
        }
        return null;
    }

    
}