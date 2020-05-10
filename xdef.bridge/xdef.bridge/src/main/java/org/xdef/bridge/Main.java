package org.xdef.bridge;

import java.io.IOException;
import java.util.Scanner;

import org.xdef.bridge.server.*;


public class Main {

    private static Listener listener;
    private static Thread listenerThread;

    public static void main(String[] args) {
        try {
            listener = new TcpListener();
        } catch (IOException ex) {
            System.err.println("Failed to initialize listener: " + ex.getMessage());
        }

        if (listener != null) {
            listenerThread = new Thread(() -> {
                listener.listen();
            });

            listenerThread.start();
            System.out.println("Listening for connections.");

            Scanner scanner = new Scanner(System.in);
            boolean readInput = true;
            while (readInput) {
                String line = scanner.nextLine();
                line = line.toLowerCase();
                switch (line) {
                    case ("exit"):
                        readInput = false;
                        listener.close();
                        break;
                    default:
                        break;
                }
            }
            scanner.close();
        }

        System.out.println("Exiting...");
        if (listenerThread.isAlive()) {
            try {
                listenerThread.join();
            } catch (InterruptedException ex) {
                // Do nothing, thread exited one way or another
            }
        }

        return;
    }
}
