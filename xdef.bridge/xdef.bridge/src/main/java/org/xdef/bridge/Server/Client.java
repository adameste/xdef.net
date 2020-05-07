/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package org.xdef.bridge.Server;

import java.io.IOException;

/**
 *
 * @author Gweana
 */
public abstract class Client {
    
    public abstract void disconnect();
    public abstract void listen() throws IOException;
}
