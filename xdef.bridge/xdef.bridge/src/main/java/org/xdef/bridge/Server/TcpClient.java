package org.xdef.bridge.server;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;


import org.xdef.bridge.server.requests.Request;


public class TcpClient extends Client {

    private Socket socket;
    private boolean shouldListen = true;

    public TcpClient(int clientId, Socket socket) {
        super(clientId);
        this.socket = socket;
    }

    @Override
    public void disconnect() {
        shouldListen = false;
        try {
            socket.close();
        } catch (IOException ex) {
            // Do nothing, already disconnected
        }
    }

    @Override
    public void listen() throws IOException {
        var inputStream = new DataInputStream(socket.getInputStream());
        while (shouldListen) {
            var request = Request.readFromStream(inputStream);
            handleRequest(request);
        }
        inputStream.close();
    }

    @Override
    protected void sendRequestData(Request request) {
        try {
            var stream = socket.getOutputStream();
            var dataOutputStream = new DataOutputStream(stream);
            request.writeToStream(dataOutputStream);
            dataOutputStream.flush();
        } catch (IOException e) {
            // Client disconnected
            disconnect();
        }
    }

}
