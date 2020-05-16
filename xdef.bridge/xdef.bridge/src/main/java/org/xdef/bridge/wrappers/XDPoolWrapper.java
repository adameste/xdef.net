package org.xdef.bridge.wrappers;

import java.io.IOException;
import org.xdef.XDDocument;
import org.xdef.XDPool;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.server.requests.ResponseException;
import org.xdef.bridge.utils.BinaryDataBuilder;
import org.xdef.bridge.utils.BinaryDataReader;

public class XDPoolWrapper extends RemoteHandlingObject {

    private static final int FUNCTION_CREATE_XDDOCUMENT = 1;
    
    private XDPool xdPool;

    public XDPoolWrapper(Client client) {
        super(client);
    }

    public XDPoolWrapper(Client client, XDPool xdPool) {
        super(client);
        this.xdPool = xdPool;
    }

    @Override
    public Response handleRequest(Request request) {
        try
        {
            BinaryDataReader reader = request.getReader();
            switch (request.getFunction())
            {
                case (FUNCTION_CREATE_XDDOCUMENT):
                    return createXDDocument(reader);
                default:
                    return new ResponseException(ResponseException.ERROR_CODE_UNKNOWN_FUNCTION, "XDPool: Unknown function.");
            }
        } catch (Exception ex) {
            return new ResponseException(ResponseException.ERROR_CODE_INVALID_REQUEST, ex.getMessage());
        }
    }

    private Response createXDDocument(BinaryDataReader reader) throws IOException {
        String arg = reader.readSharpString();
        XDDocument res;
        if (arg != null)
            res = xdPool.createXDDocument(arg);
        else
            res= xdPool.createXDDocument();
        XDDocumentWrapper wrapper = new XDDocumentWrapper(client, res);
        int id = client.registerRemoteObject(wrapper);
        return new Response(new BinaryDataBuilder().add(id).build());
    }
    
}