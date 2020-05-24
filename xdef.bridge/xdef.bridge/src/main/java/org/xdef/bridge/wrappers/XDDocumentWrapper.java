package org.xdef.bridge.wrappers;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.StringWriter;
import java.net.URL;
import java.util.Properties;

import javax.xml.namespace.QName;
import javax.xml.transform.TransformerConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.xdef.XDDocument;
import org.xdef.XDInput;
import org.xdef.XDOutput;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.EmptyResponse;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.server.requests.ResponseException;
import org.xdef.bridge.utils.BinaryDataBuilder;
import org.xdef.bridge.utils.BinaryDataReader;
import org.xdef.bridge.wrappers.streams.RemoteInputStream;
import org.xdef.bridge.wrappers.streams.RemoteOutputStream;
import org.xdef.bridge.wrappers.streams.RemoteStreamWrapper;
import org.xdef.sys.ReportWriter;
import org.xdef.sys.SRuntimeException;

public class XDDocumentWrapper extends RemoteHandlingObject {

    private static final int FUNCTION_SETPROPERTIES = 1;
    private static final int FUNCTION_SETPROPERTY = 2;
    private static final int FUNCTION_GETPROPERTIES = 3;
    private static final int FUNCTION_ISCREATEMODE = 4;
    private static final int FUNCTION_GETDOCUMENT = 5;
    private static final int FUNCTION_SETROOTMODEL = 6;
    private static final int FUNCTION_XPARSE_1 = 7;
    private static final int FUNCTION_XPARSE_2 = 8;
    private static final int FUNCTION_XPARSE_3 = 9;
    private static final int FUNCTION_XPARSE_4 = 10;
    private static final int FUNCTION_XPARSE_5 = 11;
    private static final int FUNCTION_XPARSE_6 = 12;
    private static final int FUNCTION_XPARSE_7 = 13;
    private static final int FUNCTION_XCREATE_1 = 14;
    private static final int FUNCTION_XCREATE_2 = 15;
    private static final int FUNCTION_XCREATE_3 = 16;
    private static final int FUNCTION_PARSEXCOMPONENT_1 = 17;
    private static final int FUNCTION_PARSEXCOMPONENT_2 = 18;
    private static final int FUNCTION_PARSEXCOMPONENT_3 = 19;
    private static final int FUNCTION_PARSEXCOMPONENT_4 = 20;
    private static final int FUNCTION_PARSEXCOMPONENT_5 = 21;
    private static final int FUNCTION_PARSEXCOMPONENT_6 = 22;
    private static final int FUNCTION_JPARSE_1 = 23;
    private static final int FUNCTION_JPARSE_2 = 24;
    private static final int FUNCTION_JPARSE_3 = 25;
    private static final int FUNCTION_JPARSE_4 = 26;
    private static final int FUNCTION_JPARSE_5 = 27;
    private static final int FUNCTION_JPARSE_6 = 28;
    private static final int FUNCTION_JPARSEXCOMPONENT_1 = 29;
    private static final int FUNCTION_JPARSEXCOMPONENT_2 = 30;
    private static final int FUNCTION_JPARSEXCOMPONENT_3 = 31;
    private static final int FUNCTION_JPARSEXCOMPONENT_4 = 32;
    private static final int FUNCTION_JPARSEXCOMPONENT_5 = 33;
    private static final int FUNCTION_JPARSEXCOMPONENT_6 = 34;
    private static final int FUNCTION_GETSTDOUT = 35;
    private static final int FUNCTION_GETSTDERR = 36;
    private static final int FUNCTION_GETSTDIN = 37;
    private static final int FUNCTION_SETSTREAMWRITER_1 = 38;
    private static final int FUNCTION_SETSTREAMWRITER_2 = 39;
    private static final int FUNCTION_SETSTREAMWRITER_3 = 40;
    private static final int FUNCTION_SETSTDOUT_1 = 41;
    private static final int FUNCTION_SETSTDOUT_2 = 42;
    private static final int FUNCTION_SETSTDIN_1 = 43;
    private static final int FUNCTION_SETSTDOUT_3 = 44;
    private static final int FUNCTION_SETSTDIN_2 = 45;
    private static final int FUNCTION_SETDEBUGGER = 46;
    private static final int FUNCTION_GETDEBUGGER = 47;
    private static final int FUNCTION_SETDEBUG = 48;
    private static final int FUNCTION_ISDEBUG = 49;
    private static final int FUNCTION_PREPAREROOTXXELEMENTNS = 50;
    private static final int FUNCTION_PREPAREROOTXXELEMENT = 51;
    private static final int FUNCTION_GETIMPLPROPERTIES = 52;
    private static final int FUNCTION_GETIMPLPROPERTY = 53;
    private static final int FUNCTION_ISLEGALDATE = 54;
    private static final int FUNCTION_GETMINYEAR = 55;
    private static final int FUNCTION_SETMINYEAR = 56;
    private static final int FUNCTION_GETMAXYEAR = 57;
    private static final int FUNCTION_SETMAXYEAR = 58;
    private static final int FUNCTION_GETSPECIALDATES = 59;
    private static final int FUNCTION_SETSPECIALDATES = 60;
    private static final int FUNCTION_CHECKDATELEGAL = 61;
    private static final int FUNCTION_PRINTREPORTS = 62;
    private static final int FUNCTION_GETLEXICONLANGUAGE = 63;
    private static final int FUNCTION_SETLEXICONLANGUAGE = 64;
    private static final int FUNCTION_XTRANSLATE_1 = 65;
    private static final int FUNCTION_XTRANSLATE_2 = 66;

    private final XDDocument xdDocument;

    public XDDocumentWrapper(Client client, XDDocument xdDocument) {
        super(client);
        this.xdDocument = xdDocument;
    }

    // Autogenerated handler method
    @Override
    public Response handleRequest(final Request request) {
        final BinaryDataReader reader = request.getReader();
        try {
            switch (request.getFunction()) {

                case FUNCTION_SETPROPERTIES:
                    return setProperties(reader);
                case FUNCTION_SETPROPERTY:
                    return setProperty(reader);
                case FUNCTION_GETPROPERTIES:
                    return getProperties(reader);
                case FUNCTION_ISCREATEMODE:
                    return isCreateMode(reader);
                case FUNCTION_GETDOCUMENT:
                    return getDocument(reader);
                case FUNCTION_SETROOTMODEL:
                    return setRootModel(reader);
                case FUNCTION_XPARSE_1:
                    return xparse1(reader);
                case FUNCTION_XPARSE_2:
                    return xparse2(reader);
                case FUNCTION_XPARSE_3:
                    return xparse3(reader);
                case FUNCTION_XPARSE_4:
                    return xparse4(reader);
                case FUNCTION_XPARSE_5:
                    return xparse5(reader);
                case FUNCTION_XPARSE_6:
                    return xparse6(reader);
                case FUNCTION_XPARSE_7:
                    return xparse7(reader);
                case FUNCTION_XCREATE_1:
                    return xcreate1(reader);
                case FUNCTION_XCREATE_2:
                    return xcreate2(reader);
                case FUNCTION_XCREATE_3:
                    return xcreate3(reader);
                case FUNCTION_PARSEXCOMPONENT_1:
                    return parseXComponent1(reader);
                case FUNCTION_PARSEXCOMPONENT_2:
                    return parseXComponent2(reader);
                case FUNCTION_PARSEXCOMPONENT_3:
                    return parseXComponent3(reader);
                case FUNCTION_PARSEXCOMPONENT_4:
                    return parseXComponent4(reader);
                case FUNCTION_PARSEXCOMPONENT_5:
                    return parseXComponent5(reader);
                case FUNCTION_PARSEXCOMPONENT_6:
                    return parseXComponent6(reader);
                case FUNCTION_JPARSE_1:
                    return jparse1(reader);
                case FUNCTION_JPARSE_2:
                    return jparse2(reader);
                case FUNCTION_JPARSE_3:
                    return jparse3(reader);
                case FUNCTION_JPARSE_4:
                    return jparse4(reader);
                case FUNCTION_JPARSE_5:
                    return jparse5(reader);
                case FUNCTION_JPARSE_6:
                    return jparse6(reader);
                case FUNCTION_JPARSEXCOMPONENT_1:
                    return jparseXComponent1(reader);
                case FUNCTION_JPARSEXCOMPONENT_2:
                    return jparseXComponent2(reader);
                case FUNCTION_JPARSEXCOMPONENT_3:
                    return jparseXComponent3(reader);
                case FUNCTION_JPARSEXCOMPONENT_4:
                    return jparseXComponent4(reader);
                case FUNCTION_JPARSEXCOMPONENT_5:
                    return jparseXComponent5(reader);
                case FUNCTION_JPARSEXCOMPONENT_6:
                    return jparseXComponent6(reader);
                case FUNCTION_GETSTDOUT:
                    return getStdOut(reader);
                case FUNCTION_GETSTDERR:
                    return getStdErr(reader);
                case FUNCTION_GETSTDIN:
                    return getStdIn(reader);
                case FUNCTION_SETSTREAMWRITER_1:
                    return setStreamWriter1(reader);
                case FUNCTION_SETSTREAMWRITER_2:
                    return setStreamWriter2(reader);
                case FUNCTION_SETSTREAMWRITER_3:
                    return setStreamWriter3(reader);
                case FUNCTION_SETSTDOUT_1:
                    return setStdOut1(reader);
                case FUNCTION_SETSTDOUT_2:
                    return setStdOut2(reader);
                case FUNCTION_SETSTDIN_1:
                    return setStdIn1(reader);
                case FUNCTION_SETSTDOUT_3:
                    return setStdOut3(reader);
                case FUNCTION_SETSTDIN_2:
                    return setStdIn2(reader);
                case FUNCTION_SETDEBUGGER:
                    return setDebugger(reader);
                case FUNCTION_GETDEBUGGER:
                    return getDebugger(reader);
                case FUNCTION_SETDEBUG:
                    return setDebug(reader);
                case FUNCTION_ISDEBUG:
                    return isDebug(reader);
                case FUNCTION_PREPAREROOTXXELEMENTNS:
                    return prepareRootXXElementNS(reader);
                case FUNCTION_PREPAREROOTXXELEMENT:
                    return prepareRootXXElement(reader);
                case FUNCTION_GETIMPLPROPERTIES:
                    return getImplProperties(reader);
                case FUNCTION_GETIMPLPROPERTY:
                    return getImplProperty(reader);
                case FUNCTION_ISLEGALDATE:
                    return isLegalDate(reader);
                case FUNCTION_GETMINYEAR:
                    return getMinYear(reader);
                case FUNCTION_SETMINYEAR:
                    return setMinYear(reader);
                case FUNCTION_GETMAXYEAR:
                    return getMaxYear(reader);
                case FUNCTION_SETMAXYEAR:
                    return setMaxYear(reader);
                case FUNCTION_GETSPECIALDATES:
                    return getSpecialDates(reader);
                case FUNCTION_SETSPECIALDATES:
                    return setSpecialDates(reader);
                case FUNCTION_CHECKDATELEGAL:
                    return checkDateLegal(reader);
                case FUNCTION_PRINTREPORTS:
                    return printReports(reader);
                case FUNCTION_GETLEXICONLANGUAGE:
                    return getLexiconLanguage(reader);
                case FUNCTION_SETLEXICONLANGUAGE:
                    return setLexiconLanguage(reader);
                case FUNCTION_XTRANSLATE_1:
                    return xtranslate1(reader);
                case FUNCTION_XTRANSLATE_2:
                    return xtranslate2(reader);
                default:
                    return new ResponseException(ResponseException.ERROR_CODE_UNKNOWN_FUNCTION,
                            "XDFactory: Unknown function.");
            }
        } catch (final Exception ex) {
            return new ResponseException(ResponseException.ERROR_CODE_INVALID_REQUEST, ex.getMessage());
        }
    }

    // Autogenerated method
    // public abstract void setProperties(java.util.Properties);
    private Response setProperties(BinaryDataReader reader) throws IOException {
        // Read params here
        Properties arg1 = reader.readProperties();
        // Do actions
        xdDocument.setProperties(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public abstract void setProperty(java.lang.String, java.lang.String);
    private Response setProperty(BinaryDataReader reader) throws IOException {
        // Read params here
        String arg1 = reader.readSharpString();
        String arg2 = reader.readSharpString();
        // Do actions
        xdDocument.setProperty(arg1, arg2);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public abstract java.util.Properties getProperties();
    private Response getProperties(BinaryDataReader reader) throws IOException {
        // Read params here
        // Do actions
        Properties wrap = xdDocument.getProperties();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(wrap);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract boolean isCreateMode();
    private Response isCreateMode(BinaryDataReader reader) throws IOException {
        // Read params here
        // Do actions
        boolean res = xdDocument.isCreateMode();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Document getDocument();
    private Response getDocument(BinaryDataReader reader) throws IOException {
        // Read params here
        // Do actions
        Document wrap = xdDocument.getDocument();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(wrap);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void setRootModel(org.xdef.model.XMElement);
    private Response setRootModel(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // Read params here
        // XMElement arg1 = reader.readXMElement();
        // // Do actions
        // voidWrapper wrap = new voidWrapper(client, xdDocument.setRootModel(arg1));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xparse(java.lang.String,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response xparse1(BinaryDataReader reader) throws IOException {
        // Read params here
        String arg1 = reader.readSharpString();
        ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xparse(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xparse(java.lang.String,
    // java.lang.String, org.xdef.sys.ReportWriter) throws
    // org.xdef.sys.SRuntimeException;
    private Response xparse2(BinaryDataReader reader) throws IOException {
        // Read params here
        String arg1 = reader.readSharpString();
        String arg2 = reader.readSharpString();
        ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xparse(arg1, arg2, arg3);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xparse(java.net.URL,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response xparse3(BinaryDataReader reader) throws IOException {
        // Read params here
        URL arg1 = new URL(reader.readSharpString());
        ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xparse(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xparse(java.io.File,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response xparse4(BinaryDataReader reader) throws IOException {
        // Read params here
        File arg1 = new File(reader.readSharpString());
        ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xparse(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xparse(org.w3c.dom.Node,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response xparse5(BinaryDataReader reader) throws IOException {
        // Read params here
        Node arg1 = reader.readElement();
        ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xparse(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xparse(java.io.InputStream,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response xparse6(BinaryDataReader reader) throws IOException {
        // Read params here
        InputStream arg1 = new RemoteInputStream(new RemoteStreamWrapper(client, reader.readInt()));
        ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xparse(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xparse(java.io.InputStream,
    // java.lang.String, org.xdef.sys.ReportWriter) throws
    // org.xdef.sys.SRuntimeException;
    private Response xparse7(BinaryDataReader reader) throws IOException {
        // Read params here
        InputStream arg1 = new RemoteInputStream(new RemoteStreamWrapper(client, reader.readInt()));
        String arg2 = reader.readSharpString();
        ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xparse(arg1, arg2, arg3);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xcreate(java.lang.String,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response xcreate1(BinaryDataReader reader) throws IOException {
        // Read params here
        String arg1 = reader.readSharpString();
        ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xcreate(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xcreate(java.lang.String,
    // java.lang.String, org.xdef.sys.ReportWriter) throws
    // org.xdef.sys.SRuntimeException;
    private Response xcreate2(BinaryDataReader reader) throws IOException {
        // Read params here
        String arg1 = reader.readSharpString();
        String arg2 = reader.readSharpString();
        ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xcreate(arg1, arg2, arg3);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xcreate(javax.xml.namespace.QName,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response xcreate3(BinaryDataReader reader) throws IOException {
        // Read params here
        QName arg1 = new QName(reader.readSharpString(), reader.readSharpString());
        ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xcreate(arg1, arg2);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent
    // parseXComponent(java.lang.String, java.lang.Class<?>,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response parseXComponent1(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        /*
         * /// Read params here /String arg1 = reader.readSharpString(); /Class<?> arg2
         * = reader.readClass<?>(); /ReportWriter arg3 = (ReportWriter)
         * client.getLocalObject(reader.readInt()); /// Do actions /XComponentWrapper
         * wrap = new XComponentWrapper(client,
         * xdDocument.parseXComponent(arg1,arg2,arg3)); /BinaryDataBuilder builder = new
         * BinaryDataBuilder(); /builder.add(client.registerRemoteObject(wrap)); /return
         * new Response(builder.build());
         */
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent parseXComponent(java.io.File,
    // java.lang.Class<?>, org.xdef.sys.ReportWriter) throws
    // org.xdef.sys.SRuntimeException;
    private Response parseXComponent2(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // File arg1 = new File(reader.ReadSharpString());
        // Class<?> arg2 = reader.readClass<?>();
        // ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // XComponentWrapper wrap = new XComponentWrapper(client,
        // xdDocument.parseXComponent(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent parseXComponent(java.net.URL,
    // java.lang.Class<?>, org.xdef.sys.ReportWriter) throws
    // org.xdef.sys.SRuntimeException;
    private Response parseXComponent3(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // URL arg1 = reader.readURL();
        // Class<?> arg2 = reader.readClass<?>();
        // ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // XComponentWrapper wrap = new XComponentWrapper(client,
        // xdDocument.parseXComponent(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent
    // parseXComponent(java.io.InputStream, java.lang.Class<?>,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response parseXComponent4(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // InputStream arg1 = new RemoteInputStream(new RemoteStreamWrapper(client,
        // reader.readInt()));
        // Class<?> arg2 = reader.readClass<?>();
        // ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // XComponentWrapper wrap = new XComponentWrapper(client,
        // xdDocument.parseXComponent(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent
    // parseXComponent(java.io.InputStream, java.lang.Class<?>, java.lang.String,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response parseXComponent5(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // InputStream arg1 = new RemoteInputStream(new RemoteStreamWrapper(client,
        // reader.readInt()));
        // Class<?> arg2 = reader.readClass<?>();
        // String arg3 = reader.readSharpString();
        // ReportWriter arg4 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // XComponentWrapper wrap = new XComponentWrapper(client,
        // xdDocument.parseXComponent(arg1,arg2,arg3,arg4));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent
    // parseXComponent(org.w3c.dom.Node, java.lang.Class<?>,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response parseXComponent6(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // Read params here
        // Node arg1 = reader.readNode();
        // Class<?> arg2 = reader.readClass<?>();
        // ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // XComponentWrapper wrap = new XComponentWrapper(client,
        // xdDocument.parseXComponent(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract java.lang.Object jparse(java.lang.String,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response jparse1(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // ObjectWrapper wrap = new ObjectWrapper(client, xdDocument.jparse(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract java.lang.Object jparse(java.io.File,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response jparse2(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // File arg1 = new File(reader.ReadSharpString());
        // ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // ObjectWrapper wrap = new ObjectWrapper(client, xdDocument.jparse(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract java.lang.Object jparse(java.net.URL,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response jparse3(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // URL arg1 = reader.readURL();
        // ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // ObjectWrapper wrap = new ObjectWrapper(client, xdDocument.jparse(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract java.lang.Object jparse(java.io.InputStream,
    // java.lang.String, org.xdef.sys.ReportWriter) throws
    // org.xdef.sys.SRuntimeException;
    private Response jparse4(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // InputStream arg1 = new RemoteInputStream(new RemoteStreamWrapper(client,
        // reader.readInt()));
        // String arg2 = reader.readSharpString();
        // ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // ObjectWrapper wrap = new ObjectWrapper(client,
        // xdDocument.jparse(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract java.lang.Object jparse(org.w3c.dom.Node,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response jparse5(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // Node arg1 = reader.readNode();
        // ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // ObjectWrapper wrap = new ObjectWrapper(client, xdDocument.jparse(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract java.lang.Object jparse(java.lang.Object,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response jparse6(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // Object arg1 = reader.readObject();
        // ReportWriter arg2 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // ObjectWrapper wrap = new ObjectWrapper(client, xdDocument.jparse(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent
    // jparseXComponent(java.lang.String, java.lang.Class<?>,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response jparseXComponent1(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // Class<?> arg2 = reader.readClass<?>();
        // ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // XComponentWrapper wrap = new XComponentWrapper(client,
        // xdDocument.jparseXComponent(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent jparseXComponent(java.net.URL,
    // java.lang.Class<?>, org.xdef.sys.ReportWriter) throws
    // org.xdef.sys.SRuntimeException;
    private Response jparseXComponent2(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // URL arg1 = reader.readURL();
        // Class<?> arg2 = reader.readClass<?>();
        // ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // XComponentWrapper wrap = new XComponentWrapper(client,
        // xdDocument.jparseXComponent(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent
    // jparseXComponent(java.io.InputStream, java.lang.String, java.lang.Class<?>,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response jparseXComponent3(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // InputStream arg1 = new RemoteInputStream(new RemoteStreamWrapper(client,
        // reader.readInt()));
        // String arg2 = reader.readSharpString();
        // Class<?> arg3 = reader.readClass<?>();
        // ReportWriter arg4 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // XComponentWrapper wrap = new XComponentWrapper(client,
        // xdDocument.jparseXComponent(arg1,arg2,arg3,arg4));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent jparseXComponent(java.io.File,
    // java.lang.Class<?>, org.xdef.sys.ReportWriter) throws
    // org.xdef.sys.SRuntimeException;
    private Response jparseXComponent4(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // File arg1 = new File(reader.ReadSharpString());
        // Class<?> arg2 = reader.readClass<?>();
        // ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // XComponentWrapper wrap = new XComponentWrapper(client,
        // xdDocument.jparseXComponent(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent
    // jparseXComponent(org.w3c.dom.Node, java.lang.Class<?>,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response jparseXComponent5(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // Read params here
        // Node arg1 = reader.readNode();
        // Class<?> arg2 = reader.readClass<?>();
        // ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // XComponentWrapper wrap = new XComponentWrapper(client,
        // xdDocument.jparseXComponent(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.component.XComponent
    // jparseXComponent(java.lang.Object, java.lang.Class<?>,
    // org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
    private Response jparseXComponent6(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // Object arg1 = reader.readObject();
        // Class<?> arg2 = reader.readClass<?>();
        // ReportWriter arg3 = (ReportWriter) client.getLocalObject(reader.readInt());
        // // Do actions
        // XComponentWrapper wrap = new XComponentWrapper(client,
        // xdDocument.jparseXComponent(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.XDOutput getStdOut();
    private Response getStdOut(BinaryDataReader reader) throws IOException {
        // Read params here
        // Do actions
        XDOutputWrapper wrap = new XDOutputWrapper(client, xdDocument.getStdOut());
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(client.registerRemoteObject(wrap));
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.XDOutput getStdErr();
    private Response getStdErr(BinaryDataReader reader) throws IOException {
        // Read params here
        // Do actions
        XDOutputWrapper wrap = new XDOutputWrapper(client, xdDocument.getStdErr());
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(client.registerRemoteObject(wrap));
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.XDInput getStdIn();
    private Response getStdIn(BinaryDataReader reader) throws IOException {
        // Read params here
        // Do actions
        XDInputWrapper wrap = new XDInputWrapper(client, xdDocument.getStdIn());
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(client.registerRemoteObject(wrap));
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void setStreamWriter(java.io.OutputStream, java.lang.String,
    // boolean) throws java.io.IOException;
    private Response setStreamWriter1(BinaryDataReader reader) throws IOException {
        // Read params here
        OutputStream arg1 = new RemoteOutputStream(new RemoteStreamWrapper(client, reader.readInt()));
        String arg2 = reader.readSharpString();
        boolean arg3 = reader.readBoolean();
        // Do actions
        xdDocument.setStreamWriter(arg1, arg2, arg3);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public abstract void setStreamWriter(java.io.Writer, java.lang.String,
    // boolean);
    private Response setStreamWriter2(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // Writer arg1 = reader.readWriter();
        // String arg2 = reader.readSharpString();
        // boolean arg3 = reader.read boolean();
        // // Do actions
        // voidWrapper wrap = new voidWrapper(client,
        // xdDocument.setStreamWriter(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void setStreamWriter(org.xdef.XDXmlOutStream);
    private Response setStreamWriter3(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // XDXmlOutStream arg1 = reader.readXDXmlOutStream();
        // // Do actions
        // voidWrapper wrap = new voidWrapper(client, xdDocument.setStreamWriter(arg1));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void setStdOut(java.io.PrintStream);
    private Response setStdOut1(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // Read params here
        // PrintStream arg1 = reader.readPrintStream();
        // // Do actions
        // voidWrapper wrap = new voidWrapper(client, xdDocument.setStdOut(arg1));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void setStdOut(java.io.Writer);
    private Response setStdOut2(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // Read params here
        // Writer arg1 = reader.readWriter();
        // // Do actions
        // voidWrapper wrap = new voidWrapper(client, xdDocument.setStdOut(arg1));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void setStdIn(java.io.InputStream);
    private Response setStdIn1(BinaryDataReader reader) throws IOException {
        // Read params here
        InputStream arg1 = new RemoteInputStream(new RemoteStreamWrapper(client, reader.readInt()));
        // Do actions
        xdDocument.setStdIn(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public abstract void setStdOut(org.xdef.XDOutput);
    private Response setStdOut3(BinaryDataReader reader) throws IOException {
        // Read params here
        XDOutput arg1 = ((XDOutputWrapper) client.getLocalObject(reader.readInt())).getXdOutput();
        // Do actions
        xdDocument.setStdOut(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public abstract void setStdIn(org.xdef.XDInput);
    private Response setStdIn2(BinaryDataReader reader) throws IOException {
        // Read params here
        XDInput arg1 = ((XDInputWrapper) client.getLocalObject(reader.readInt())).getXdInput();
        // Do actions
        xdDocument.setStdIn(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public abstract void setDebugger(org.xdef.XDDebug);
    private Response setDebugger(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // XDDebug arg1 = reader.readXDDebug();
        // // Do actions
        // voidWrapper wrap = new voidWrapper(client, xdDocument.setDebugger(arg1));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.XDDebug getDebugger();
    private Response getDebugger(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // // Do actions
        // XDDebugWrapper wrap = new XDDebugWrapper(client, xdDocument.getDebugger());
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void setDebug(boolean);
    private Response setDebug(BinaryDataReader reader) throws IOException {
        // Read params here
        boolean arg1 = reader.readBoolean();
        // Do actions
        xdDocument.setDebug(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public abstract boolean isDebug();
    private Response isDebug(BinaryDataReader reader) throws IOException {
        // Read params here
        // Do actions
        boolean res = xdDocument.isDebug();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.proc.XXElement
    // prepareRootXXElementNS(java.lang.String, java.lang.String, boolean);
    private Response prepareRootXXElementNS(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // String arg2 = reader.readSharpString();
        // boolean arg3 = reader.readBoolean();
        // // Do actions
        // XXElementWrapper wrap = new XXElementWrapper(client,
        // xdDocument.prepareRootXXElementNS(arg1,arg2,arg3));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.xdef.proc.XXElement
    // prepareRootXXElement(java.lang.String, boolean);
    private Response prepareRootXXElement(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // String arg1 = reader.readSharpString();
        // boolean arg2 = reader.read boolean();
        // // Do actions
        // XXElementWrapper wrap = new XXElementWrapper(client,
        // xdDocument.prepareRootXXElement(arg1,arg2));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract java.util.Properties getImplProperties();
    private Response getImplProperties(BinaryDataReader reader) throws IOException {
        // Read params here
        // Do actions
        Properties wrap = xdDocument.getImplProperties();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(wrap);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract java.lang.String getImplProperty(java.lang.String);
    private Response getImplProperty(BinaryDataReader reader) throws IOException {
        // Read params here
        String arg1 = reader.readSharpString();
        // Do actions
        String res = xdDocument.getImplProperty(arg1);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract boolean isLegalDate(org.xdef.sys.SDatetime);
    private Response isLegalDate(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // SDatetime arg1 = reader.readSDatetime();
        // // Do actions
        // boolean res = xdDocument.isLegalDate(arg1);
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(res);
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract int getMinYear();
    private Response getMinYear(BinaryDataReader reader) throws IOException {
        // Read params here
        // Do actions
        int res = xdDocument.getMinYear();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void setMinYear(int);
    private Response setMinYear(BinaryDataReader reader) throws IOException {
        // Read params here
        int arg1 = reader.readInt();
        // Do actions
        xdDocument.setMinYear(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public abstract int getMaxYear();
    private Response getMaxYear(BinaryDataReader reader) throws IOException {
        // Read params here
        // Do actions
        int res = xdDocument.getMaxYear();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void setMaxYear(int);
    private Response setMaxYear(BinaryDataReader reader) throws IOException {
        // Read params here
        int arg1 = reader.readInt();
        // Do actions
        xdDocument.setMaxYear(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public abstract org.xdef.sys.SDatetime[] getSpecialDates();
    private Response getSpecialDates (BinaryDataReader reader) throws IOException
    {
        throw new UnsupportedOperationException();
        // // Read params here
        // // Do actions
        // SDatetime[]Wrapper wrap = new SDatetime[]Wrapper(client, xdDocument.getSpecialDates());
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void setSpecialDates(org.xdef.sys.SDatetime[]);
    private Response setSpecialDates (BinaryDataReader reader) throws IOException
    {
        throw new UnsupportedOperationException();
        // // Read params here
        // SDatetime[] arg1 = reader.readSDatetime[]();
        // // Do actions
        // voidWrapper wrap = new voidWrapper(client, xdDocument.setSpecialDates(arg1));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void checkDateLegal(boolean);
    private Response checkDateLegal(BinaryDataReader reader) throws IOException {
        // Read params here
        boolean arg1 = reader.readBoolean();
        // Do actions
        xdDocument.checkDateLegal(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public abstract void printReports(java.io.PrintStream);
    private Response printReports(BinaryDataReader reader) throws IOException {
        throw new UnsupportedOperationException();
        // // Read params here
        // PrintStream arg1 = reader.readPrintStream();
        // // Do actions
        // voidWrapper wrap = new voidWrapper(client, xdDocument.printReports(arg1));
        // BinaryDataBuilder builder = new BinaryDataBuilder();
        // builder.add(client.registerRemoteObject(wrap));
        // return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract java.lang.String getLexiconLanguage();
    private Response getLexiconLanguage(BinaryDataReader reader) throws IOException {
        // Read params here
        // Do actions
        String res = xdDocument.getLexiconLanguage();
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract void setLexiconLanguage(java.lang.String) throws
    // org.xdef.sys.SRuntimeException;
    private Response setLexiconLanguage(BinaryDataReader reader) throws IOException {
        // Read params here
        String arg1 = reader.readSharpString();
        // Do actions
        xdDocument.setLexiconLanguage(arg1);
        return new EmptyResponse();
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xtranslate(java.lang.String,
    // java.lang.String, java.lang.String, org.xdef.sys.ReportWriter) throws
    // org.xdef.sys.SRuntimeException;
    private Response xtranslate1(BinaryDataReader reader) throws IOException {
        // Read params here
        String arg1 = reader.readSharpString();
        String arg2 = reader.readSharpString();
        String arg3 = reader.readSharpString();
        ReportWriter arg4 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xtranslate(arg1, arg2, arg3, arg4);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

    // Autogenerated method
    // public abstract org.w3c.dom.Element xtranslate(org.w3c.dom.Element,
    // java.lang.String, java.lang.String, org.xdef.sys.ReportWriter) throws
    // org.xdef.sys.SRuntimeException;
    private Response xtranslate2(BinaryDataReader reader) throws IOException {
        // Read params here
        Element arg1 = reader.readElement();
        String arg2 = reader.readSharpString();
        String arg3 = reader.readSharpString();
        ReportWriter arg4 = (ReportWriter) client.getLocalObject(reader.readInt());
        // Do actions
        Element res = xdDocument.xtranslate(arg1, arg2, arg3, arg4);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(res);
        return new Response(builder.build());
    }

}
