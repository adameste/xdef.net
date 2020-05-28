package org.xdef.bridge.wrappers.sys;

import org.xdef.bridge.server.Client;
import org.xdef.sys.ArrayReporter;

public class ArrayReporterWrapper extends ReporterWrapper {

    private ArrayReporter arrayReporter;

    public ArrayReporterWrapper(Client client, ArrayReporter arrayReporter) {
        super(client);
        reportReader = arrayReporter;
        reportWriter = arrayReporter;
        reportReaderWrapper = new ReportReaderWrapper(client, reportReader, true);
        reportWriterWrapper = new ReportWriterWrapper(client, reportWriter, true);
    }

    public ArrayReporterWrapper(Client client) {
        this(client, new ArrayReporter());
    }

    public ArrayReporter getArrayReporter() {
        return arrayReporter;
    }

}
