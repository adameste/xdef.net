package org.xdef.bridge.server;

import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketException;

public class TcpListener extends Listener {

    private ServerSocket serverSocket;

    private boolean shouldListen = true;

    public TcpListener(int port) throws IOException {
        serverSocket = new ServerSocket(port, 50, InetAddress.getByName("127.0.0.1"));

    }

    public TcpListener() throws IOException {
        this(42268);
    }

    @Override
    public void listen() {
        while (shouldListen) {
            try {
                Socket socket = serverSocket.accept();
                setupSocket(socket);
                Client client = new TcpClient(socket);
                listenToClient(client);
            } catch (IOException ex) {
                if (shouldListen) {
                    System.err.println("Accept socket error:" + ex.getMessage());
                } else {
                } // Do nothing
            }
        }
    }

    private void setupSocket(Socket socket) {
        try {
            socket.setSoTimeout(0);
        } catch (SocketException e) {
            // Do nothing
        }
    }

    @Override
    public void close() {
        shouldListen = false;
        try {
            if (!serverSocket.isClosed()) {
                serverSocket.close();
            }
            
        } catch (IOException ex) {
        } // Do nothing, already closed/not open
        super.close();
    }
}
