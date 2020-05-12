package org.xdef.bridge.server.requests;

public class Response extends Request{

    private static final int RESPONSE_FUNCTION = 1;

    public Response(byte[] data) {
        super(RESPONSE_FUNCTION, data);
    }
    
}