package org.xdef.bridge.server;

import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketException;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.Executor;
import java.util.concurrent.Executors;
import java.util.concurrent.locks.ReentrantLock;

public class TcpListener extends Listener {

    private ServerSocket serverSocket;
    private Executor executor;
    private boolean shouldListen = true;
    private Map<Integer, Client> clients = new HashMap<Integer, Client>();
    private ReentrantLock clientsLock = new ReentrantLock();
    private int currentClientId = 1;

    public TcpListener(int port) throws IOException {
        serverSocket = new ServerSocket(port, 50, InetAddress.getByName("127.0.0.1"));
        executor = Executors.newCachedThreadPool();
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
                executor.execute(() -> {
                    Client client = new TcpClient(currentClientId++, socket);
                    AddClient(client);
                    try {
                        client.listen();
                    } catch (IOException ex) {
                    } // Do nothing & end
                    RemoveClient(client);

                });
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
            clientsLock.lock();
            for (Client c : clients.values()) {
                c.disconnect();
            }
            clientsLock.unlock();
        } catch (IOException ex) {
        } // Do nothing, already closed/not open
    }

    private void AddClient(Client client) {
        clientsLock.lock();
        clients.put(client.getClientId(), client);
        clientsLock.unlock();
    }

    private void RemoveClient(Client client) {
        clientsLock.lock();
        clients.remove(client.getClientId());
        clientsLock.unlock();
    }

}
