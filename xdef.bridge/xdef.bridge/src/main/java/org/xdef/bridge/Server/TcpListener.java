/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package org.xdef.bridge.Server;

import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.HashSet;
import java.util.concurrent.Executor;
import java.util.concurrent.Executors;
import java.util.concurrent.locks.ReentrantLock;

/**
 *
 * @author Gweana
 */
public class TcpListener extends Listener {

    private ServerSocket _serverSocket;
    private Executor _executor;
    private boolean _shouldListen = true;
    private HashSet<Client> _clients = new HashSet<Client>();
    private ReentrantLock _clientsLock = new ReentrantLock();

    public TcpListener(int port) throws IOException {
        _serverSocket = new ServerSocket(port, 50, InetAddress.getLocalHost());
        _executor = Executors.newCachedThreadPool();
    }

    public TcpListener() throws IOException {
        this(42268);
    }

    @Override
    public void listen() {
        while (_shouldListen) {
            try {
                Socket socket = _serverSocket.accept();
                _executor.execute(() -> {
                    Client client = new TcpClient(socket);
                    AddClient(client);
                    try {
                        client.listen();
                    } catch (IOException ex) {
                    } // Do nothing & end
                    RemoveClient(client);

                });
            } catch (IOException ex) {
                if (_shouldListen) {
                    System.err.println("Accept socket error:" + ex.getMessage());
                } else {
                } // Do nothing
            }
        }
    }

    @Override
    public void close() {
        _shouldListen = false;
        try {
            if (!_serverSocket.isClosed()) {
                _serverSocket.close();
            }
            _clientsLock.lock();
            for (Client c : _clients) {
                c.disconnect();
            }
            _clientsLock.unlock();
        } catch (IOException ex) {
        } // Do nothing, already closed/not open
    }

    private void AddClient(Client client) {
        _clientsLock.lock();
        _clients.add(client);
        _clientsLock.unlock();
    }

    private void RemoveClient(Client client) {
        _clientsLock.lock();
        _clients.remove(client);
        _clientsLock.unlock();
    }

}
