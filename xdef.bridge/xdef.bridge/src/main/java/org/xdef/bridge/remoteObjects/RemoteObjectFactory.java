package org.xdef.bridge.remoteObjects;

import java.nio.charset.StandardCharsets;

import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.wrappers.XDFactoryWrapper;
import org.xdef.bridge.wrappers.sys.ArrayReporterWrapper;

public class RemoteObjectFactory {
    private final static String OBJECT_XDFACTORY = "XDFactory";
    private final static String OBJECT_ARRAYREPORTER = "ArrayReporter";


    private Client client;

    public RemoteObjectFactory(Client client) {
        this.client = client;
    }

    public RemoteHandlingObject createObject(Request request) {
        String objectName = new String(request.getData(), StandardCharsets.UTF_8);
        switch (objectName) {
            case OBJECT_XDFACTORY:
                return new XDFactoryWrapper(client);
            case OBJECT_ARRAYREPORTER:
                return new ArrayReporterWrapper(client);
            default:
                return null;
        }
    }
}