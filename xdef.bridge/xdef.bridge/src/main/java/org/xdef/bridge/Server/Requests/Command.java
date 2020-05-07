/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package org.xdef.bridge.Server.Requests;


/**
 *
 * @author Gweana
 */
public class Command {
    private Function _function;
    private byte[] _data;

    public byte[] getData() {
        return _data;
    }

    public void setData(byte[] _data) {
        this._data = _data;
    }

    public Function getFunction() {
        return _function;
    }

    public void setFunction(Function _function) {
        this._function = _function;
    }
}
