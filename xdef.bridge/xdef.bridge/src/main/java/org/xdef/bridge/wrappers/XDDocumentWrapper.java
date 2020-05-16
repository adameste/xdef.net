package org.xdef.bridge.wrappers;

import java.io.File;
import java.io.IOException;
import java.io.StringWriter;
import javax.xml.transform.TransformerConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import org.w3c.dom.Element;
import org.xdef.XDDocument;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.server.requests.ResponseException;
import org.xdef.bridge.utils.BinaryDataBuilder;
import org.xdef.bridge.utils.BinaryDataReader;
import org.xdef.sys.ReportWriter;
import org.xdef.sys.SRuntimeException;

public class XDDocumentWrapper extends RemoteHandlingObject {

    private static final int FUNCTION_XPARSE = 1;
    
    private static final int XPARSE_OVERLOAD_FILE = 1;
    
    private final XDDocument xdDocument;

    public XDDocumentWrapper(Client client, XDDocument xdDocument) {
        super(client);
        this.xdDocument = xdDocument;
    }
    
    
    @Override
    public Response handleRequest(Request request) {
        BinaryDataReader reader = request.getReader();
        try {
            switch(request.getFunction())
            {
                case FUNCTION_XPARSE:
                    return xParse(reader);
                default:
                    return new ResponseException(ResponseException.ERROR_CODE_UNKNOWN_FUNCTION, "XDDocument: invalid function.");
            }
        } catch (SRuntimeException ex) {
            return new ResponseException(ResponseException.ERROR_CODE_S_RUNTIME_EXCEPTION, ex.getMessage());
        } catch (Exception ex){
            return new ResponseException(ResponseException.ERROR_CODE_INVALID_REQUEST, ex.getMessage());
        }
    }

    private Response xParse(BinaryDataReader reader) throws IOException, TransformerConfigurationException, TransformerException {
        int overload = reader.readInt();
        Element result = null;
        switch(overload) {
            case XPARSE_OVERLOAD_FILE:
                result = xParseFile(reader);
                break;
            default:
                return new ResponseException(ResponseException.ERROR_CODE_UNKNOWN_FUNCTION, "XDDocument.xParse unknown overload.");
        }
        if (result != null)
        {
            StringWriter stringWriter = new StringWriter();
            TransformerFactory.newInstance().newTransformer().transform(new DOMSource(result), new StreamResult(stringWriter));
            stringWriter.flush();
            return new Response(new BinaryDataBuilder().add(stringWriter.toString()).build());
        }
        else
            return new Response(new BinaryDataBuilder().add("").build());
    }

    private Element xParseFile(BinaryDataReader reader) throws IOException {
        String path = reader.readSharpString();
        int writerId = reader.readInt();
        ReportWriter reportWriter = null;
        if (writerId != 0)
            reportWriter = (ReportWriter) client.getLocalObject(writerId);
        return xdDocument.xparse(new File(path), reportWriter);
    }

}
