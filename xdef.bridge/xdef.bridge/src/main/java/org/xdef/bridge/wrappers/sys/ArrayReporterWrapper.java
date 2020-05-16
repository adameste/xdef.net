package org.xdef.bridge.wrappers.sys;

import org.xdef.bridge.server.Client;
import org.xdef.sys.ArrayReporter;

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
