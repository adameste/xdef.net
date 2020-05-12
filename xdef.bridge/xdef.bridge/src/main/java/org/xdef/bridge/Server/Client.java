package org.xdef.bridge.server;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.TreeMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.locks.ReentrantLock;

import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.remoteObjects.RemoteObject;
import org.xdef.bridge.remoteObjects.RemoteObjectFactory;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.RequestWaiter;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.server.requests.ResponseException;
import org.xdef.bridge.utils.BinaryDataBuilder;

public abstract class Client {

    private int clientId;
    private int serverRequestId = 1;
    private int remoteObjectId = 1;
    private final Map<Integer, RequestWaiter> waitingRequests = new TreeMap<Integer, RequestWaiter>();
    private final Map<Integer, RemoteHandlingObject> remoteObjects = new HashMap<>();

    private ExecutorService threadPool = Executors.newCachedThreadPool();
    private ReentrantLock sendLock = new ReentrantLock();

    public static final int FUNCTION_CREATE_OBJECT = 1;
    public static final int FUNCTION_DELETE_OBJECT = 2;

    public Client(int clientId) {
        this.clientId = clientId;
    }

    public int getClientId() {
        return clientId;
    }

    public abstract void disconnect();

    public abstract void listen() throws IOException;

    protected abstract void sendRequestData(Request request);

    public void sendRequestWithoutResponse(Request request) {
        sendLock.lock();
        request.setServerRequestId(serverRequestId++);
        sendRequestData(request);
        sendLock.unlock();
    }

    public Request sendRequestWithResponse(Request request) {
        RequestWaiter waiter = new RequestWaiter(serverRequestId);
        waitingRequests.put(serverRequestId, waiter);
        sendRequestWithoutResponse(request);
        try {
            waiter.getSemaphore().acquire();
        } catch (InterruptedException e) {
            // Not expected to fail, do nothing and return null
        }
        return waiter.getResponse();
    }

    protected void handleRequest(Request request) {
        threadPool.submit(() -> {
            Response response = null;
            if (waitingRequests.containsKey(request.getServerRequestId())) {
                RequestWaiter waiter = waitingRequests.get(request.getServerRequestId());
                waiter.setResponse(request);
                waitingRequests.remove(request.getServerRequestId());
                waiter.getSemaphore().release();
            } else if (request.getObjectId() == 0) {
                response = handleObjectlessRequest(request);
            } else {
                RemoteHandlingObject obj = remoteObjects.get(request.getObjectId());
                if (obj == null)
                    response = new ResponseException(ResponseException.ERROR_CODE_UNKNOWN_OBJECT, "Unknown remote object.");
                else
                    response = obj.handleRequest(request);
            }
            if (response != null) {
                response.setClientRequestId(request.getClientRequestId());
                response.setObjectId(request.getObjectId());
                sendRequestWithoutResponse(response);
            }
        });
    }

    public int registerRemoteObject(RemoteHandlingObject obj) {
        obj.setObjectId(remoteObjectId);
        remoteObjectId++;
        remoteObjects.put(obj.getObjectId(), obj);
        return obj.getObjectId();

    }

    public void deleteRemoteObject(RemoteObject obj) {
        byte[] data = new BinaryDataBuilder().add(obj.getObjectId()).build();
        sendRequestWithoutResponse(new Request(FUNCTION_DELETE_OBJECT, data));
    }


    private Response handleObjectlessRequest(Request request) {
        switch (request.getFunction()) {
            case FUNCTION_CREATE_OBJECT:
                return createObject(request);
            case FUNCTION_DELETE_OBJECT:
                try {
                    remoteObjects.remove(request.getReader().readInt());
                } catch (IOException e) {
                   
                }
                return null;
            default:
                return null;
        }
    }

    private Response createObject(Request request) {
        RemoteObjectFactory remoteObjectFactory = new RemoteObjectFactory(this);
        RemoteHandlingObject obj = remoteObjectFactory.createObject(request);
        registerRemoteObject(obj);
        BinaryDataBuilder builder = new BinaryDataBuilder();
        builder.add(obj.getObjectId());
        Response response = new Response(builder.build());
        return response;
    }

}
