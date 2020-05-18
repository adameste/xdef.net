package org.xdef.bridge.server.requests;

public class RemoteCallException extends Exception{

    private static final long serialVersionUID = 3051187634212893330L;
    private int errorCode;
    
    public RemoteCallException(int errorCode, String message) {
        super(message);
        this.errorCode = errorCode;
        
    }

    public int getErrorCode() {
        return errorCode;
    }
    
}
