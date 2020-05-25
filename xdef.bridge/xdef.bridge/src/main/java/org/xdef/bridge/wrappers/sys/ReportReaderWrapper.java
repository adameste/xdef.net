package org.xdef.bridge.wrappers.sys;

import java.io.IOException;
import java.io.PrintStream;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.EmptyResponse;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.server.requests.ResponseException;
import org.xdef.bridge.utils.BinaryDataBuilder;
import org.xdef.bridge.utils.BinaryDataReader;
import org.xdef.bridge.wrappers.streams.RemoteOutputStream;
import org.xdef.bridge.wrappers.streams.RemoteStreamWrapper;
import org.xdef.sys.Report;
import org.xdef.sys.ReportReader;
import org.xdef.sys.ReportWriter;

public class ReportReaderWrapper{

    private final static int FUNCTION_READER_REPORT = 1;
    private final static int FUNCTION_READER_CLOSE = 2;
    private final static int FUNCTION_READER_PRINT_REPORTS = 3;
    private final static int FUNCTION_READER_PRINT_TO_STRING = 4;
    private final static int FUNCTION_READER_WRITE_REPORTS= 5;
    
    private final ReportReader reportReader;
    private Client client;

    public ReportReaderWrapper(Client client, ReportReader reader) {
        this.client = client;
        this.reportReader = reader;
    }

    
    public Response handleRequest(Request request) {
        try
        {
            BinaryDataReader reader = request.getReader();
            switch (request.getFunction())
            {
                case FUNCTION_READER_REPORT:
                    return getReport(reader);
                case FUNCTION_READER_CLOSE:
                    return close(reader);
                case FUNCTION_READER_PRINT_REPORTS:
                    return printReports(reader);
                case FUNCTION_READER_PRINT_TO_STRING:
                    return printToString(reader);
                case FUNCTION_READER_WRITE_REPORTS:
                    return writeReports(reader);
                default:
                    return new ResponseException(ResponseException.ERROR_CODE_UNKNOWN_FUNCTION, "ReportWriter unknown funciton.");
            }
        } catch (Exception ex)
        {
            return new ResponseException(ResponseException.ERROR_CODE_INVALID_REQUEST, ex.getMessage());
        }
    }

    private Response getReport(BinaryDataReader reader) {
        Report report = reportReader.getReport();
        ReportWrapper reportWrapper = new ReportWrapper(client, report);
        int id = client.registerRemoteObject(reportWrapper);
        return new Response(new BinaryDataBuilder().add(id).build());
    }

    private Response close(BinaryDataReader reader) {
        reportReader.close();
        return new EmptyResponse();
    }

    private Response printReports(BinaryDataReader reader) throws IOException {
        int streamId = reader.readInt();
        RemoteOutputStream outputStream = new RemoteOutputStream(new RemoteStreamWrapper(client, streamId));
        String lang = reader.readSharpString();
        if (lang == null)
        {
            reportReader.printReports(new PrintStream(outputStream));
        }
        else
        {
            reportReader.printReports(new PrintStream(outputStream), lang);
        }
        outputStream.flush();
        return new EmptyResponse();           
    }

    private Response printToString(BinaryDataReader reader) throws IOException {
        String lang = reader.readSharpString();
        String str;
        if (lang == null)
            str = reportReader.printToString();
        else
            str = reportReader.printToString(lang);
        return new Response(new BinaryDataBuilder().add(str).build());
    }

    private Response writeReports(BinaryDataReader reader) throws IOException {
        int readerId = reader.readInt();
        ReportWriter reportWriter = (ReportWriter) client.getLocalObject(readerId);
        reportReader.writeReports(reportWriter);
        return new EmptyResponse();
    }


}
