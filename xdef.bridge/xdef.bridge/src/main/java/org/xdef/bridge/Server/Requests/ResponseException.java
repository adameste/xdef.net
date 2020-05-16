package org.xdef.bridge.server.requests;

import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.xdef.bridge.utils.BinaryDataBuilder;
import org.xdef.bridge.utils.BinaryDataReader;

public class ResponseException extends Response {

    private static final int RESPONSE_FUNCTION_EXCEPTION = 2;

    public static final int ERROR_CODE_UNKNOWN_OBJECT = 1;
    public static final int ERROR_CODE_UNKNOWN_FUNCTION= 2;
    public static final int ERROR_CODE_INVALID_REQUEST = 3;
    public static final int ERROR_CODE_S_RUNTIME_EXCEPTION = 4;




    public ResponseException(int errorCode, String message) 
    {
        super(null);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(errorCode);
        builder.add(message);
        setData(builder.build());
        setFunction(RESPONSE_FUNCTION_EXCEPTION);

    }
    
    public static boolean isException(Request request) {
        return request.getFunction() == RESPONSE_FUNCTION_EXCEPTION;
    }
    
    public static RemoteCallException getException(Request request) {
        BinaryDataReader reader = request.getReader();
        try {
            return new RemoteCallException(reader.readInt(), reader.readSharpString());
        } catch (IOException ex) {
            return null;
        }
    }
    
}