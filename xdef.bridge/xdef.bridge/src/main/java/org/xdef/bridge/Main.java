/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package org.xdef.bridge;

import java.io.IOException;
import java.util.Scanner;
import org.w3c.dom.Element;
import org.xdef.XDDocument;
import org.xdef.XDFactory;
import org.xdef.XDPool;
import org.xdef.bridge.Server.*;
import org.xdef.sys.ArrayReporter;

/**
 *
 * @author Gweana
 */
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
            switch (line){
                case ("exit"):
                    readInput = false;
                    _listener.close();
                    break;
                default:
                    break;
            }
        }
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
