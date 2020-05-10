package org.xdef.bridge.server.requests;

import java.util.concurrent.Semaphore;

public class RequestWaiter {
    private final int requestId;
    private final Semaphore semaphore = new Semaphore(0);
    private Request response;

    public RequestWaiter(int requestId) {
        this.requestId = requestId;
    }

    public Request getResponse() {
        return response;
    }

    public void setResponse(Request response) {
        this.response = response;
    }

    public int getRequestId() {
        return requestId;
    }

    public Semaphore getSemaphore() {
        return semaphore;
    }

    
    
}