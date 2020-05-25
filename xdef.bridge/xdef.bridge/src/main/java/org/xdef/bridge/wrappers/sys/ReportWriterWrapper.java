package org.xdef.bridge.wrappers.sys;

import java.io.IOException;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.EmptyResponse;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.server.requests.ResponseException;
import org.xdef.bridge.utils.BinaryDataBuilder;
import org.xdef.bridge.utils.BinaryDataReader;
import org.xdef.sys.Report;
import org.xdef.sys.ReportReader;
import org.xdef.sys.ReportWriter;
import org.xdef.sys.SRuntimeException;

public class ReportWriterWrapper {

    private final static int FUNCTION_WRITER_LANGUAGE = 1001;
    private final static int FUNCTION_WRITER_LAST_ERROR_REPORT = 1002;
    private final static int FUNCTION_WRITER_SIZE = 1003;
    private final static int FUNCTION_WRITER_FATALS = 1004;
    private final static int FUNCTION_WRITER_ERRORS = 1005;
    private final static int FUNCTION_WRITER_ERRORWARNINGS = 1006;
    private final static int FUNCTION_WRITER_FATAL_COUNT = 1007;
    private final static int FUNCTION_WRITER_ERROR_COUNT = 1008;
    private final static int FUNCTION_WRITER_LIGHT_ERROR_COUNT = 1009;
    private final static int FUNCTION_WRITER_WARNING_COUNT = 1010;
    private final static int FUNCTION_WRITER_ADD_REPORTS = 1011;
    private final static int FUNCTION_WRITER_CHECK_AND_THROW_ERRORS = 1012;
    private final static int FUNCTION_WRITER_CHECK_AND_THROW_ERROR_WARNINGS = 1013;
    private final static int FUNCTION_WRITER_CLEAR = 1014;
    private final static int FUNCTION_WRITER_CLEAR_COUNTERS = 1015;
    private final static int FUNCTION_WRITER_CLEAR_LAST_ERROR_REPORT = 1016;
    private final static int FUNCTION_WRITER_CLOSE = 1017;

    private final ReportWriter reportWriter;
    private Client client;

    public ReportWriterWrapper(Client client, ReportWriter writer) {
        this.client = client;
        this.reportWriter = writer;
    }

    public Response handleRequest(Request request) {
        BinaryDataReader reader = request.getReader();
        try
        {
        switch (request.getFunction()) {
            case FUNCTION_WRITER_LANGUAGE:
                return setLanguage(reader);
            case FUNCTION_WRITER_LAST_ERROR_REPORT:
                return getLastErrorReport(reader);
            case FUNCTION_WRITER_SIZE:
                return getSize(reader);
            case FUNCTION_WRITER_FATALS:
                return getFatals(reader);
            case FUNCTION_WRITER_ERRORS:
                return getErrors(reader);
            case FUNCTION_WRITER_ERRORWARNINGS:
                return getErrorWarnings(reader);
            case FUNCTION_WRITER_FATAL_COUNT:
                return getWriterFatalCount(reader);
            case FUNCTION_WRITER_ERROR_COUNT:
                return getFunctionWriterErrorCount(reader);
            case FUNCTION_WRITER_LIGHT_ERROR_COUNT:
                return getWriterLightErrorCount(reader);
            case FUNCTION_WRITER_WARNING_COUNT:
                return getWarningCount(reader);
            case FUNCTION_WRITER_ADD_REPORTS:
                return addReports(reader);
            case FUNCTION_WRITER_CHECK_AND_THROW_ERRORS:
                return checkAndThrowErrors(reader);
            case FUNCTION_WRITER_CHECK_AND_THROW_ERROR_WARNINGS:
                return checkAndThrowErrorWarnings(reader);
            case FUNCTION_WRITER_CLEAR:
                return clear(reader);
            case FUNCTION_WRITER_CLEAR_COUNTERS:
                return clearCounters(reader);
            case FUNCTION_WRITER_CLEAR_LAST_ERROR_REPORT:
                return clearLastErrorReport(reader);
            case FUNCTION_WRITER_CLOSE:
                return close(reader);
            default:
                return new ResponseException(ResponseException.ERROR_CODE_UNKNOWN_FUNCTION, "ReportWriter unknown function.");
        }
        } catch (Exception ex) {
            return new ResponseException(ResponseException.ERROR_CODE_INVALID_REQUEST, "ReportWriter invalid request.");
        }
    }

    private Response setLanguage(BinaryDataReader reader) throws IOException {
        String lang = reader.readSharpString();
        reportWriter.setLanguage(lang);
        return new EmptyResponse();
    }

    private Response getLastErrorReport(BinaryDataReader reader) {
        Report report = reportWriter.getLastErrorReport();
        ReportWrapper reportWrapper = new ReportWrapper(client, report);
        int id = client.registerRemoteObject(reportWrapper);
        return new Response(new BinaryDataBuilder().add(id).build());
    }

    private Response getSize(BinaryDataReader reader) {
        int x = reportWriter.size();
        return new Response(new BinaryDataBuilder().add(x).build());
    }

    private Response getFatals(BinaryDataReader reader) {
        boolean x = reportWriter.fatals();
        return new Response(new BinaryDataBuilder().add(x).build());
    }

    private Response getErrors(BinaryDataReader reader) {
        boolean x = reportWriter.errors();
        return new Response(new BinaryDataBuilder().add(x).build());
    }

    private Response getErrorWarnings(BinaryDataReader reader) {
        boolean x = reportWriter.errorWarnings();
        return new Response(new BinaryDataBuilder().add(x).build());
    }

    private Response getWriterFatalCount(BinaryDataReader reader) {
        int x = reportWriter.getFatalErrorCount();
        return new Response(new BinaryDataBuilder().add(x).build());
    }

    private Response getFunctionWriterErrorCount(BinaryDataReader reader) {
        int x = reportWriter.getErrorCount();
        return new Response(new BinaryDataBuilder().add(x).build());
    }

    private Response getWriterLightErrorCount(BinaryDataReader reader) {
        int x = reportWriter.getLightErrorCount();
        return new Response(new BinaryDataBuilder().add(x).build());
    }

    private Response getWarningCount(BinaryDataReader reader) {
        int x = reportWriter.getWarningCount();
        return new Response(new BinaryDataBuilder().add(x).build());
    }

    private Response addReports(BinaryDataReader reader) throws IOException {
        int readerId = reader.readInt();
        ReportReader reportReader = (ReportReader) client.getLocalObject(readerId);
        reportWriter.addReports(reportReader);
        return new EmptyResponse();
    }

    private Response checkAndThrowErrors(BinaryDataReader reader) {
        try
        {
            reportWriter.checkAndThrowErrors();
            return new EmptyResponse();
        } catch (SRuntimeException ex)
        {
            return new Response(new BinaryDataBuilder().add(ex.getMessage()).build());
        }
    }

    private Response checkAndThrowErrorWarnings(BinaryDataReader reader) {
        try
        {
            reportWriter.checkAndThrowErrorWarnings();
            return new EmptyResponse();
        } catch (SRuntimeException ex)
        {
            return new Response(new BinaryDataBuilder().add(ex.getMessage()).build());
        }
    }
    
    private Response clear(BinaryDataReader reader) {
        reportWriter.clear();
        return new EmptyResponse();
    }

    private Response clearCounters(BinaryDataReader reader) {
        reportWriter.clearCounters();
        return new EmptyResponse();
    }

    private Response clearLastErrorReport(BinaryDataReader reader) {
        reportWriter.clearLastErrorReport();
        return new EmptyResponse();
    }

    private Response close(BinaryDataReader reader) {
        reportWriter.close();
        return new EmptyResponse();
    }

    

}
