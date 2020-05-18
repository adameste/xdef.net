package org.xdef.bridge.server;

import java.io.DataOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.Socket;


import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.utils.CustomDataInputStream;


public class TcpClient extends Client {

    private Socket socket;
    private boolean shouldListen = true;

    public TcpClient(Socket socket) {
        super();
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
        CustomDataInputStream inputStream = new CustomDataInputStream(socket.getInputStream());
        while (shouldListen) {
            Request request = Request.readFromStream(inputStream);
            handleRequest(request);
        }
        inputStream.close();
    }

    @Override
    protected void sendRequestData(Request request) {
        try {
            OutputStream stream = socket.getOutputStream();
            DataOutputStream dataOutputStream = new DataOutputStream(stream);
            synchronized(stream){
                request.writeToStream(dataOutputStream);
                dataOutputStream.flush();
            }
        } catch (IOException e) {
            // Client disconnected
            disconnect();
        }
    }

}
