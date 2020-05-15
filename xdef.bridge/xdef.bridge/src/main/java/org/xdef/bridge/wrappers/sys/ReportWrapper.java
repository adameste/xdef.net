package org.xdef.bridge.wrappers.sys;

import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.sys.Report;

public class ReportWrapper extends RemoteHandlingObject{

    private final Report report;

    public ReportWrapper(Client client, Report report) {
        super(client);
        this.report = report;       
    }
    
    @Override
    public Response handleRequest(Request request) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

}
