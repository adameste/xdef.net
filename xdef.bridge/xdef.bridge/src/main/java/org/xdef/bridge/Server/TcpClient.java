package org.xdef.bridge.Server;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.Socket;
import java.util.Scanner;

import org.xdef.bridge.Server.Requests.Request;
import org.xdef.bridge.Server.Requests.Function;

public class TcpClient extends Client {

    private Socket _socket;
    private boolean _shouldListen = true;
    private ClientRequestProcessor _requestProcessor;

    public TcpClient(Socket socket) {
        _socket = socket;
        _requestProcessor = new ClientRequestProcessor(this);
    }

    @Override
    public void disconnect() {
        _shouldListen = false;
        try {
            _socket.close();
        } catch (IOException ex) {
            // Do nothing, already disconnected
        }
    }

    @Override
    public void listen() throws IOException {
        var inputStream = new DataInputStream(_socket.getInputStream());
        var functions = Function.values();
        while (_shouldListen) {
            var function = inputStream.readInt();
            var payload = inputStream.readInt();
            var bytes = inputStream.readNBytes(payload);
            if (function > functions.length) {
                System.err.println("Unknown function number: " + function);
            }
            else {
                var cmd = new Request(functions[function], bytes);
                _requestProcessor.processCommand(cmd);
            }
            
        }
        inputStream.close();
    }

    @Override
    public void sendPacketWithoutResponse(Request request) {
        try {
            var stream = _socket.getOutputStream();
            var dataOutputStream = new DataOutputStream(stream);
            request.writeToStream(dataOutputStream);
            dataOutputStream.flush();            
        } catch (IOException ex) {
            disconnect(); // Connection lost
        }

    }

    @Override
    public Request sendPacketWithResponse(Request request){
        // TODO Auto-generated method stub
        return null;
    }
}
