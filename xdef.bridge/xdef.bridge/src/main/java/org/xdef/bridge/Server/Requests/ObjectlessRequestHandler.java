package org.xdef.bridge.server.requests;

import java.io.IOException;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.remoteObjects.RemoteObjectFactory;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.utils.BinaryDataBuilder;

public class ObjectlessRequestHandler {
    
    public static final int FUNCTION_CREATE_OBJECT = 1;
    public static final int FUNCTION_DELETE_OBJECT = 2;
    
    private final Client client;
    
    public ObjectlessRequestHandler(Client client) {
        this.client = client;
    }
    
    public Response handleRequest(Request request) {
        switch (request.getFunction()) {
            case FUNCTION_CREATE_OBJECT:
                return createObject(request);
            case FUNCTION_DELETE_OBJECT:
                try {
                    client.deleteLocalObject(request.getReader().readInt());
                } catch (IOException ex) {
                    // DO nothing
                }
                return null;
            default:
                return null;
        }
    }
    
     

    private Response createObject(Request request) {
        RemoteObjectFactory remoteObjectFactory = new RemoteObjectFactory(client);
        RemoteHandlingObject obj = remoteObjectFactory.createObject(request);
        client.registerRemoteObject(obj);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(obj.getObjectId());
        Response response = new Response(builder.build());
        return response;
    }  

}
