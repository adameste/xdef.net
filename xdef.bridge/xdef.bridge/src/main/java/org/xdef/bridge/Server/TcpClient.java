/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package org.xdef.bridge.Server;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.net.Socket;
import java.nio.ByteBuffer;
import java.util.Scanner;

/**
 *
 * @author Gweana
 */
public class TcpClient extends Client {

    private Socket _socket;
    private boolean _shouldListen = true;

    public TcpClient(Socket socket) {
        _socket = socket;
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
        InputStream stream = _socket.getInputStream();
        var scanner = new Scanner(stream);
        while (_shouldListen) {
            var command = scanner.nextInt();
            var payload = scanner.nextInt();
            var bytes = stream.readNBytes(payload);
        }
    }
}
