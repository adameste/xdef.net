package org.xdef.bridge;

import java.io.IOException;
import java.util.Scanner;
import org.xdef.bridge.Server.*;

public class Main {

    private static Listener _listener;
    private static Thread _listenerThread;

    public static void main(String[] args) {
        try {
            _listener = new TcpListener();
        } catch (IOException ex) {
            System.err.println("Failed to initialize listener: " + ex.getMessage());
        }

        if (_listener != null) {
            _listenerThread = new Thread(() -> {
                _listener.listen();
            });

            _listenerThread.start();
            System.out.println("Listening for connections.");

            Scanner scanner = new Scanner(System.in);
            boolean readInput = true;
            while (readInput) {
                String line = scanner.nextLine();
                line = line.toLowerCase();
                switch (line) {
                    case ("exit"):
                        readInput = false;
                        _listener.close();
                        break;
                    default:
                        break;
                }
            }
            scanner.close();
        }

        System.out.println("Exiting...");
        if (_listenerThread.isAlive()) {
            try {
                _listenerThread.join();
            } catch (InterruptedException ex) {
                // Do nothing, thread exited one way or another
            }
        }

        return;
    }
}
