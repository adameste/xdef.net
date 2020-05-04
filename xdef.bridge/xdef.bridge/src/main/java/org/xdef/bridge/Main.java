/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package org.xdef.bridge;

import org.w3c.dom.Element;
import org.xdef.XDDocument;
import org.xdef.XDFactory;
import org.xdef.XDPool;
import org.xdef.sys.ArrayReporter;

/**
 *
 * @author Gweana
 */
public class Main {
    
    static final XDPool pool = XDFactory.compileXD(null, "<xd:def xmlns:xd=\"http://www.xdef.org/xdef/3.2\" name=\"Order\" root=\"Order\"> \n" +
" \n" +
"<Order Number=\"int\" CustomerCode=\"string(1,20)\">     <DeliveryPlace>     <Address Street=\"string(2,100)\"       House=\"int(1,9999)\"       City=\"string(2,100)\"       ZIP=\"num(5)\"/>   </DeliveryPlace>   <Item xd:script=\"1..10\" ProductCode=\"num(4)\" Quantity=\"int(1,1000)\"/> </Order> \n" +
" \n" +
"</xd:def> ");
    
    public static void main(String[] args) {
        
    XDDocument xdoc = pool.createXDDocument();
    ArrayReporter reporter = new ArrayReporter();
    Element result = xdoc.xparse("<Order Number=\"123\" CustomerCode=\"ALFA\">   <DeliveryPlace>    "
            + " <Address Street=\"Oldroad\" House=\"5\" City=\"NewTown\" ZIP=\"32321\" />   </DeliveryPlace>  "
            + " <Item ProductCode=\"0002\" Quantity=\"2\" />   <Item ProductCode=\"0003\" Quantity=\"1\" /> </Order>", reporter);
    return;
    }
}
