package org.xdef.bridge.remoteObjects;

import java.nio.charset.StandardCharsets;

import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.wrappers.XDFactoryWrapper;

public class RemoteObjectFactory {
    private final static String OBJECT_XDFACTORY = "XDFactory";

    private Client client;

    public RemoteObjectFactory(Client client) {
        this.client = client;
    }

    public RemoteObject createObject(Request request) {
        var objectName = new String(request.getData(), StandardCharsets.UTF_8);
        switch (objectName) {
            case OBJECT_XDFACTORY:
                return new XDFactoryWrapper(client);
            default:
                return null;
        }
    }
}