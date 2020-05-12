package org.xdef.bridge.server.requests;

public class RemoteCallException extends Exception{

    private int errorCode;
    
    public RemoteCallException(int errorCode, String message) {
        super(message);
        this.errorCode = errorCode;
        
    }

    public int getErrorCode() {
        return errorCode;
    }
    
}
