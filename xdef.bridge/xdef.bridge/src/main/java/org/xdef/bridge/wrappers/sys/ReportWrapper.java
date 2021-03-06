package org.xdef.bridge.wrappers.sys;

import java.io.IOException;

import javax.naming.OperationNotSupportedException;

import org.junit.runner.manipulation.InvalidOrderingException;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.EmptyResponse;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.server.requests.ResponseException;
import org.xdef.bridge.utils.BinaryDataBuilder;
import org.xdef.bridge.utils.BinaryDataReader;
import org.xdef.sys.Report;

public class ReportWrapper extends RemoteHandlingObject {

    private static final int FUNCTION_GENMODIFICATION = 1;
    private static final int FUNCTION_TOSTRING_1 = 2;
    private static final int FUNCTION_TOSTRING_2 = 3;
    private static final int FUNCTION_GETTYPE = 4;
    private static final int FUNCTION_GETMSGID = 5;
    private static final int FUNCTION_GETTEXT = 6;
    private static final int FUNCTION_SETTEXT = 7;
    private static final int FUNCTION_GETTIMESTAMP = 8;
    private static final int FUNCTION_SETTIMESTAMP_1 = 9;
    private static final int FUNCTION_SETTIMESTAMP_2 = 10;
    private static final int FUNCTION_GETMODIFICATION = 11;
    private static final int FUNCTION_SETMODIFICATION = 12;
    private static final int FUNCTION_GETPARAMETER = 13;
    private static final int FUNCTION_SETPARAMETER = 14;
    private static final int FUNCTION_GETLOCALIZEDTEXT_1 = 15;
    private static final int FUNCTION_GETLOCALIZEDTEXT_2 = 16;
    private static final int FUNCTION_TOXMLSTRING = 17;
    private static final int FUNCTION_TOXMLELEMENT = 18;
    private static final int FUNCTION_SETLANGUAGE = 19;
    private static final int FUNCTION_AUDIT_1 = 20;
    private static final int FUNCTION_FATAL_1 = 21;
    private static final int FUNCTION_ERROR_1 = 22;
    private static final int FUNCTION_LIGHTERROR_1 = 23;
    private static final int FUNCTION_WARNING_1 = 24;
    private static final int FUNCTION_MESSAGE_1 = 25;
    private static final int FUNCTION_INFO_1 = 26;
    private static final int FUNCTION_STRING_1 = 27;
    private static final int FUNCTION_TEXT_1 = 28;
    private static final int FUNCTION_GETRAWREPORTTEXT_1 = 29;
    private static final int FUNCTION_GETREPORTTEXT_1 = 30;
    private static final int FUNCTION_GETREPORTTEXT_2 = 31;
    private static final int FUNCTION_GETLOCALIZEDTEXT_3 = 32;
    private static final int FUNCTION_GETREPORTPARAMNAMES = 33;
    private static final int FUNCTION_GETLOCALIZEDTEXT_4 = 34;
    private static final int FUNCTION_AUDIT_2 = 35;
    private static final int FUNCTION_FATAL_2 = 36;
    private static final int FUNCTION_ERROR_2 = 37;
    private static final int FUNCTION_LIGHTERROR_2 = 38;
    private static final int FUNCTION_WARNING_2 = 39;
    private static final int FUNCTION_MESSAGE_2 = 40;
    private static final int FUNCTION_INFO_2 = 41;
    private static final int FUNCTION_STRING_2 = 42;
    private static final int FUNCTION_TEXT_2 = 43;
    private static final int FUNCTION_GETREPORTID = 44;
    private static final int FUNCTION_GETRAWREPORTTEXT_2 = 45;
    private static final int FUNCTION_GETREPORTTEXT_3 = 46;
    private static final int FUNCTION_GETREPORTTEXT_4 = 47;
    private static final int FUNCTION_BUILDINFO = 48;
    private static final int FUNCTION_WRITEOBJ = 49;
    private static final int FUNCTION_READOBJ = 50;

    private final Report report;

    public ReportWrapper(Client client, Report report) {
        super(client);
        this.report = report;
    }

    // Autogenerated handler method
    @Override
    public Response handleRequest(final Request request) {
        final BinaryDataReader reader = request.getReader();
        try {
            switch (request.getFunction()) {

                case FUNCTION_GENMODIFICATION:
                    return genModification(reader);
                case FUNCTION_TOSTRING_1:
                    return toString1(reader);
                case FUNCTION_TOSTRING_2:
                    return toString2(reader);
                case FUNCTION_GETTYPE:
                    return getType(reader);
                case FUNCTION_GETMSGID:
                    return getMsgID(reader);
                case FUNCTION_GETTEXT:
                    return getText(reader);
                case FUNCTION_SETTEXT:
                    return setText(reader);
                case FUNCTION_GETTIMESTAMP:
                    return getTimestamp(reader);
                case FUNCTION_SETTIMESTAMP_1:
                    return setTimestamp1(reader);
                case FUNCTION_SETTIMESTAMP_2:
                    return setTimestamp2(reader);
                case FUNCTION_GETMODIFICATION:
                    return getModification(reader);
                case FUNCTION_SETMODIFICATION:
                    return setModification(reader);
                case FUNCTION_GETPARAMETER:
                    return getParameter(reader);
                case FUNCTION_SETPARAMETER:
                    return setParameter(reader);
                case FUNCTION_GETLOCALIZEDTEXT_1:
                    return getLocalizedText1(reader);
                case FUNCTION_GETLOCALIZEDTEXT_2:
                    return getLocalizedText2(reader);
                case FUNCTION_TOXMLSTRING:
                    return toXmlString(reader);
                case FUNCTION_TOXMLELEMENT:
                    return toXmlElement(reader);
                case FUNCTION_SETLANGUAGE:
                    return setLanguage(reader);
                case FUNCTION_AUDIT_1:
                    return audit1(reader);
                case FUNCTION_FATAL_1:
                    return fatal1(reader);
                case FUNCTION_ERROR_1:
                    return error1(reader);
                case FUNCTION_LIGHTERROR_1:
                    return lightError1(reader);
                case FUNCTION_WARNING_1:
                    return warning1(reader);
                case FUNCTION_MESSAGE_1:
                    return message1(reader);
                case FUNCTION_INFO_1:
                    return info1(reader);
                case FUNCTION_STRING_1:
                    return string1(reader);
                case FUNCTION_TEXT_1:
                    return text1(reader);
                case FUNCTION_GETRAWREPORTTEXT_1:
                    return getRawReportText1(reader);
                case FUNCTION_GETREPORTTEXT_1:
                    return getReportText1(reader);
                case FUNCTION_GETREPORTTEXT_2:
                    return getReportText2(reader);
                case FUNCTION_GETLOCALIZEDTEXT_3:
                    return getLocalizedText3(reader);
                case FUNCTION_GETREPORTPARAMNAMES:
                    return getReportParamNames(reader);
                case FUNCTION_GETLOCALIZEDTEXT_4:
                    return getLocalizedText4(reader);
                case FUNCTION_AUDIT_2:
                    return audit2(reader);
                case FUNCTION_FATAL_2:
                    return fatal2(reader);
                case FUNCTION_ERROR_2:
                    return error2(reader);
                case FUNCTION_LIGHTERROR_2:
                    return lightError2(reader);
                case FUNCTION_WARNING_2:
                    return warning2(reader);
                case FUNCTION_MESSAGE_2:
                    return message2(reader);
                case FUNCTION_INFO_2:
                    return info2(reader);
                case FUNCTION_STRING_2:
                    return string2(reader);
                case FUNCTION_TEXT_2:
                    return text2(reader);
                case FUNCTION_GETREPORTID:
                    return getReportID(reader);
                case FUNCTION_GETRAWREPORTTEXT_2:
                    return getRawReportText2(reader);
                case FUNCTION_GETREPORTTEXT_3:
                    return getReportText3(reader);
                case FUNCTION_GETREPORTTEXT_4:
                    return getReportText4(reader);
                case FUNCTION_BUILDINFO:
                    return buildInfo(reader);
                case FUNCTION_WRITEOBJ:
                    return writeObj(reader);
                case FUNCTION_READOBJ:
                    return readObj(reader);
                default:
                    return new ResponseException(ResponseException.ERROR_CODE_UNKNOWN_FUNCTION,
                            "XDFactory: Unknown function.");
            }
        } catch (final Exception ex) {
            return new ResponseException(ResponseException.ERROR_CODE_INVALID_REQUEST, ex.getMessage());
        }
    }

    // Autogenerated method
    // public static java.lang.String genModification(java.lang.Object...)
    private Response genModification (BinaryDataReader reader) throws IOException, OperationNotSupportedException
    {
        throw new OperationNotSupportedException();
        // // Read params here
        // Object... arg1 = reader.readObject...();
        // // Do actions
        // String res = Report.genModification(arg1);
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(res);
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public java.lang.String toString()
    private Response toString1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        // Do actions
        String res = report.toString();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public java.lang.String toString(java.lang.String)
    private Response toString2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        String arg1 = reader.readSharpString();
        // Do actions
        String res = report.toString(arg1);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public final byte getType()
    private Response getType(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        // Do actions
        byte res = report.getType();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public final java.lang.String getMsgID()
    private Response getMsgID(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        // Do actions
        String res = report.getMsgID();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public final java.lang.String getText()
    private Response getText(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        // Do actions
        String res = report.getText();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public final void setText(java.lang.String)
    private Response setText(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        String arg1 = reader.readSharpString();
        // Do actions
        report.setText(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public final long getTimestamp()
    private Response getTimestamp(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        // Do actions
        long res = report.getTimestamp();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public final void setTimestamp()
    private Response setTimestamp1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        // Do actions
        report.setTimestamp();
        return new EmptyResponse();
    }

    // Autogenerated method
    // public final void setTimestamp(long)
    private Response setTimestamp2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        long arg1 = reader.readLong();
        // Do actions
        report.setTimestamp(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public final java.lang.String getModification()
    private Response getModification(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        // Do actions
        String res = report.getModification();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public final void setModification(java.lang.String)
    private Response setModification(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        String arg1 = reader.readSharpString();
        // Do actions
        report.setModification(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public final java.lang.String getParameter(java.lang.String)
    private Response getParameter(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        String arg1 = reader.readSharpString();
        // Do actions
        String res = report.getParameter(arg1);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public final void setParameter(java.lang.String, java.lang.String)
    private Response setParameter(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        String arg1 = reader.readSharpString();
        String arg2 = reader.readSharpString();
        // Do actions
        report.setParameter(arg1, arg2);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public final java.lang.String getLocalizedText()
    private Response getLocalizedText1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        // Do actions
        String res = report.getLocalizedText();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public final java.lang.String getLocalizedText(java.lang.String)
    private Response getLocalizedText2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        String arg1 = reader.readSharpString();
        // Do actions
        String res = report.getLocalizedText(arg1);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public final java.lang.String toXmlString()
    private Response toXmlString(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        // Do actions
        String res = report.toXmlString();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public final org.w3c.dom.Element toXmlElement(org.w3c.dom.Document)
    private Response toXmlElement(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        Document arg1 = reader.readDocument();
        // Do actions
        Element res = report.toXmlElement(arg1);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public static java.lang.Object setLanguage(java.lang.String)
    private Response setLanguage(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // // Do actions
        // ObjectWrapper wrap = new ObjectWrapper(client, Report.setLanguage(arg1));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report audit(java.lang.String, java.lang.String,
    // java.lang.Object...)
    private Response audit1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // String arg2 = reader.readSharpString();
        // Object... arg3 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.audit(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report fatal(java.lang.String, java.lang.String,
    // java.lang.Object...)
    private Response fatal1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // String arg2 = reader.readSharpString();
        // Object... arg3 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.fatal(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report error(java.lang.String, java.lang.String,
    // java.lang.Object...)
    private Response error1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // String arg2 = reader.readSharpString();
        // Object... arg3 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.error(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report lightError(java.lang.String,
    // java.lang.String, java.lang.Object...)
    private Response lightError1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // String arg2 = reader.readSharpString();
        // Object... arg3 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client,
        // Report.lightError(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report warning(java.lang.String, java.lang.String,
    // java.lang.Object...)
    private Response warning1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // String arg2 = reader.readSharpString();
        // Object... arg3 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client,
        // Report.warning(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report message(java.lang.String, java.lang.String,
    // java.lang.Object...)
    private Response message1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // String arg2 = reader.readSharpString();
        // Object... arg3 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client,
        // Report.message(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report info(java.lang.String, java.lang.String,
    // java.lang.Object...)
    private Response info1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // String arg2 = reader.readSharpString();
        // Object... arg3 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.info(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report string(java.lang.String, java.lang.String,
    // java.lang.Object...)
    private Response string1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // String arg2 = reader.readSharpString();
        // Object... arg3 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client,
        // Report.string(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report text(java.lang.String, java.lang.String,
    // java.lang.Object...)
    private Response text1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // String arg2 = reader.readSharpString();
        // Object... arg3 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.text(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static java.lang.String getRawReportText(java.lang.String,
    // java.lang.String)
    private Response getRawReportText1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        String arg1 = reader.readSharpString();
        String arg2 = reader.readSharpString();
        // Do actions
        String res = Report.getRawReportText(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public static java.lang.String getReportText(java.lang.String,
    // java.lang.String)
    private Response getReportText1(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        String arg1 = reader.readSharpString();
        String arg2 = reader.readSharpString();
        // Do actions
        String res = Report.getReportText(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public static java.lang.String getReportText(java.lang.String)
    private Response getReportText2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        String arg1 = reader.readSharpString();
        // Do actions
        String res = Report.getReportText(arg1);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public static java.lang.String getLocalizedText(java.lang.String,
    // java.lang.String, java.lang.String, java.lang.String)
    private Response getLocalizedText3(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        String arg1 = reader.readSharpString();
        String arg2 = reader.readSharpString();
        String arg3 = reader.readSharpString();
        String arg4 = reader.readSharpString();
        // Do actions
        String res = Report.getLocalizedText(arg1, arg2, arg3, arg4);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public static java.lang.String[] getReportParamNames(java.lang.String,
    // java.lang.String)
    private Response getReportParamNames(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        String arg1 = reader.readSharpString();
        String arg2 = reader.readSharpString();
        // Do actions
        String[] res = Report.getReportParamNames(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public static java.lang.String getLocalizedText(long, java.lang.String,
    // java.lang.String)
    private Response getLocalizedText4(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        long arg1 = reader.readLong();
        String arg2 = reader.readSharpString();
        String arg3 = reader.readSharpString();
        // Do actions
        String res = Report.getLocalizedText(arg1, arg2, arg3);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report audit(long, java.lang.Object...)
    private Response audit2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // long arg1 = reader.readLong();
        // Object... arg2 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.audit(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report fatal(long, java.lang.Object...)
    private Response fatal2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // long arg1 = reader.readLong();
        // Object... arg2 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.fatal(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report error(long, java.lang.Object...)
    private Response error2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // long arg1 = reader.readLong();
        // Object... arg2 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.error(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report lightError(long, java.lang.Object...)
    private Response lightError2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // long arg1 = reader.readLong();
        // Object... arg2 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.lightError(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report warning(long, java.lang.Object...)
    private Response warning2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // long arg1 = reader.readLong();
        // Object... arg2 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.warning(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report message(long, java.lang.Object...)
    private Response message2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // long arg1 = reader.readLong();
        // Object... arg2 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.message(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report info(long, java.lang.Object...)
    private Response info2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // long arg1 = reader.readLong();
        // Object... arg2 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.info(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report string(long, java.lang.Object...)
    private Response string2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // long arg1 = reader.readLong();
        // Object... arg2 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.string(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report text(long, java.lang.Object...)
    private Response text2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // long arg1 = reader.readLong();
        // Object... arg2 = reader.readObject...();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.text(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static java.lang.String getReportID(long)
    private Response getReportID(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        long arg1 = reader.readLong();
        // Do actions
        String res = Report.getReportID(arg1);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public static java.lang.String getRawReportText(long, java.lang.String)
    private Response getRawReportText2(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        long arg1 = reader.readLong();
        String arg2 = reader.readSharpString();
        // Do actions
        String res = Report.getRawReportText(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public static java.lang.String getReportText(long, java.lang.String)
    private Response getReportText3(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        long arg1 = reader.readLong();
        String arg2 = reader.readSharpString();
        // Do actions
        String res = Report.getReportText(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public static java.lang.String getReportText(long)
    private Response getReportText4(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        long arg1 = reader.readLong();
        // Do actions
        String res = Report.getReportText(arg1);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report buildInfo()
    private Response buildInfo(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        // Read params here
        // Do actions
        ReportWrapper wrap = new ReportWrapper(client, Report.buildInfo());
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(client.registerRemoteObject(wrap));
        return new Response(builder.build());
    }

    // Autogenerated method
    // public void writeObj(org.xdef.sys.SObjectWriter)
    private Response writeObj(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // SObjectWriter arg1 = reader.readSObjectWriter();
        // // Do actions
        // void res = report.writeObj(arg1);
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(res);
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public static org.xdef.sys.Report readObj(org.xdef.sys.SObjectReader)
    private Response readObj(BinaryDataReader reader) throws IOException, OperationNotSupportedException {
        throw new OperationNotSupportedException();
        // // Read params here
        // SObjectReader arg1 = reader.readSObjectReader();
        // // Do actions
        // ReportWrapper wrap = new ReportWrapper(client, Report.readObj(arg1));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    public Report getReport() {
        return report;
    }

}
