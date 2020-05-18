package org.xdef.bridge.server;

import java.io.IOException;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.Executor;
import java.util.concurrent.Executors;
import java.util.concurrent.atomic.AtomicInteger;

public abstract class Listener {
    private Map<Integer, Client> clients = new ConcurrentHashMap<Integer, Client>();
    private AtomicInteger currentClientId = new AtomicInteger(0);
    private Executor executor;

    public Listener() {
        super();
        executor = Executors.newCachedThreadPool();
    }

    protected void listenToClient(Client client) {
        executor.execute(() -> {
            int clientId = RegisterClient(client);
            try {
                client.listen();
            } catch (IOException ex) {
            } // Do nothing & end
            RemoveClient(clientId);
        });
    }

    public abstract void listen();

    public void close() {
        for (Client c : clients.values()) {
            c.disconnect();
        }
    }

    private int RegisterClient(Client client) {
        int clientId = currentClientId.incrementAndGet();
        clients.put(clientId, client);
        return clientId;
    }

    private void RemoveClient(int clientId) {
        clients.remove(clientId);
    }
}
