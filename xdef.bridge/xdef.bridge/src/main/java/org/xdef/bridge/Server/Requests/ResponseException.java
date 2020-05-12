package org.xdef.bridge.server.requests;

import org.xdef.bridge.utils.BinaryDataBuilder;

public class ResponseException extends Response {

    private static final int RESPONSE_FUNCTION_EXCEPTION = 2;

    public static final int ERROR_CODE_UNKNOWN_OBJECT = 1;

    public ResponseException(int errorCode, String message) 
    {
        super(null);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(errorCode);
        builder.add(message);
        setData(builder.build());
        setFunction(RESPONSE_FUNCTION_EXCEPTION);

    }
    
}