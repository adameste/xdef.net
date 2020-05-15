package org.xdef.bridge.wrappers.sys;

import java.io.PrintStream;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.sys.Report;
import org.xdef.sys.ReportReader;
import org.xdef.sys.ReportWriter;
import org.xdef.sys.SRuntimeException;

public abstract class ReporterWrapper extends RemoteHandlingObject implements ReportReader, ReportWriter {

    protected ReportReader reportReader;
    protected ReportWriter reportWriter;
    protected ReportReaderWrapper reportReaderWrapper;
    protected ReportWriterWrapper reportWriterWrapper;

    public ReporterWrapper(Client client) {
        super(client);
    }

    
    @Override
    public Report getReport() {
        return reportReader.getReport();
    }

    @Override
    public void close() {
        reportWriter.close();
    }

    @Override
    public void printReports(PrintStream arg0) {
        reportReader.printReports(arg0);
    }

    @Override
    public void printReports(PrintStream arg0, String arg1) {
        reportReader.printReports(arg0, arg1);
    }

    @Override
    public String printToString() {
        return reportReader.printToString();
    }

    @Override
    public String printToString(String arg0) {
        return reportReader.printToString(arg0);
    }

    @Override
    public void writeReports(ReportWriter arg0) {
        reportReader.writeReports(arg0);
    }

    @Override
    public void setLanguage(String arg0) {
        reportWriter.setLanguage(arg0);
    }

    @Override
    public void putReport(Report arg0) {
        reportWriter.putReport(arg0);
    }

    @Override
    public void fatal(String arg0, String arg1, Object... arg2) {
        reportWriter.fatal(arg1, arg1, arg2);
    }

    @Override
    public void fatal(long arg0, Object... arg1) {
        reportWriter.fatal(arg0, arg1);
    }

    @Override
    public void error(String arg0, String arg1, Object... arg2) {
        reportWriter.error(arg1, arg1, arg2);
    }

    @Override
    public void error(long arg0, Object... arg1) {
        reportWriter.error(arg0, arg1);
    }

    @Override
    public void lightError(long arg0, Object... arg1) {
        reportWriter.lightError(arg0, arg1);
    }

    @Override
    public void lighterror(String arg0, String arg1, Object... arg2) {
        reportWriter.lighterror(arg1, arg1, arg2);
    }

    @Override
    public void warning(String arg0, String arg1, Object... arg2) {
        reportWriter.warning(arg1, arg1, arg2);
    }

    @Override
    public void warning(long arg0, Object... arg1) {
        reportWriter.warning(arg0, arg1);
    }

    @Override
    public void audit(String arg0, String arg1, Object... arg2) {
        reportWriter.audit(arg1, arg1, arg2);
    }

    @Override
    public void audit(long arg0, Object... arg1) {
        reportWriter.audit(arg0, arg1);
    }

    @Override
    public void message(String arg0, String arg1, Object... arg2) {
        reportWriter.message(arg1, arg1, arg2);
    }

    @Override
    public void mesage(long arg0, Object... arg1) {
        reportWriter.mesage(arg0, arg1);
    }

    @Override
    public void info(String arg0, String arg1, Object... arg2) {
        reportWriter.info(arg1, arg1, arg2);
    }

    @Override
    public void info(long arg0, Object... arg1) {
        reportWriter.info(arg0, arg1);
    }

    @Override
    public void text(String arg0, String arg1, Object... arg2) {
        reportWriter.text(arg1, arg1, arg2);
    }

    @Override
    public void text(long arg0, Object... arg1) {
        reportWriter.text(arg0, arg1);
    }

    @Override
    public void string(String arg0, String arg1, Object... arg2) {
        reportWriter.string(arg1, arg1, arg2);
    }

    @Override
    public void string(long arg0, Object... arg1) {
        reportWriter.string(arg0, arg1);
    }

    @Override
    public Report getLastErrorReport() {
        return reportWriter.getLastErrorReport();
    }

    @Override
    public void clearLastErrorReport() {
        reportWriter.clearLastErrorReport();
    }

    @Override
    public void clearCounters() {
        reportWriter.clearCounters();
    }

    @Override
    public void clear() {
        reportWriter.clear();
    }

    @Override
    public int size() {
        return reportWriter.size();
    }

    @Override
    public boolean fatals() {
        return reportWriter.fatals();
    }

    @Override
    public boolean errors() {
        return reportWriter.errors();
    }

    @Override
    public boolean errorWarnings() {
        return reportWriter.errorWarnings();
    }

    @Override
    public int getFatalErrorCount() {
        return reportWriter.getFatalErrorCount();
    }

    @Override
    public int getErrorCount() {
        return reportWriter.getErrorCount();

    }

    @Override
    public int getLightErrorCount() {
        return reportWriter.getLightErrorCount();
    }

    @Override
    public int getWarningCount() {
        return reportWriter.getWarningCount();
    }

    @Override
    public ReportReader getReportReader() {
        return reportWriter.getReportReader();
    }

    @Override
    public void flush() {
        reportWriter.flush();
    }

    @Override
    public void writeString(String arg0) {
        reportWriter.writeString(arg0);
    }

    @Override
    public void checkAndThrowErrors() throws SRuntimeException {
        reportWriter.checkAndThrowErrors();
    }

    @Override
    public void checkAndThrowErrorWarnings() throws SRuntimeException {
        reportWriter.checkAndThrowErrorWarnings();
    }

    @Override
    public void addReports(ReportReader arg0) {
        reportWriter.addReports(arg0);
    }

    @Override
    public Response handleRequest(Request request) {
        if (request.getFunction() < 1000)
            return reportReaderWrapper.handleRequest(request);
        else
            return reportWriterWrapper.handleRequest(request);
    }
    

}
