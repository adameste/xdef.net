package org.xdef.bridge.remoteObjects;

import java.nio.charset.StandardCharsets;

import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.wrappers.XDFactoryWrapper;
import org.xdef.bridge.wrappers.sys.ArrayReporterWrapper;
import org.xdef.bridge.wrappers.sys.ReportWrapper;

public class RemoteObjectFactory {
    private final static String OBJECT_XDFACTORY = "XDFactory";
    private final static String OBJECT_ARRAYREPORTER = "ArrayReporter";
    private final static String OBJECT_STATIC_REPORT = "StaticReport";


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
                case OBJECT_STATIC_REPORT:
                return new ReportWrapper(client, null);
            default:
                return null;
        }
    }
}