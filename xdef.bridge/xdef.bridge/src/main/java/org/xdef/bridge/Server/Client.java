package org.xdef.bridge.Server;

import java.io.IOException;

import org.xdef.bridge.Server.Requests.Request;

public abstract class Client {

    public abstract void disconnect();

    public abstract void listen() throws IOException;
    public abstract void sendPacketWithoutResponse(Request request);
    public abstract Request sendPacketWithResponse(Request request);
}
