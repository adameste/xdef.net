package org.xdef.bridge.wrappers.sys;

import java.io.PrintStream;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.sys.ArrayReporter;
import org.xdef.sys.Report;
import org.xdef.sys.ReportReader;
import org.xdef.sys.ReportWriter;
import org.xdef.sys.SRuntimeException;

public class ArrayReporterWrapper extends ReporterWrapper{

    private ArrayReporter arrayReporter;
    
    public ArrayReporterWrapper(Client client) {
        super(client);
        arrayReporter = new ArrayReporter();
        reportReader = arrayReporter;
        reportWriter = arrayReporter;
        reportReaderWrapper = new ReportReaderWrapper(client, getObjectId(), reportReader);
        reportWriterWrapper = new ReportWriterWrapper(client, getObjectId(), reportWriter);
    }
    
}
