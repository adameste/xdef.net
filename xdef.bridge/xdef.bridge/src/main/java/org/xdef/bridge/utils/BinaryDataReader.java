package org.xdef.bridge.utils;

import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.w3c.dom.Element;
import org.xml.sax.SAXException;

public class BinaryDataReader extends CustomDataInputStream {

    public BinaryDataReader(InputStream in) {
        super(in);
    }

    public String readSharpString() throws IOException {
        int payload = readInt();
        if (payload == 0)
            return null;
        byte[] data = readNBytes(payload);
        return new String(data, StandardCharsets.UTF_8);
    }

    public Element readElement() throws IOException {
        String xml = this.readSharpString();
        try {
            DocumentBuilder documentBuilder = DocumentBuilderFactory.newInstance().newDocumentBuilder();
            return documentBuilder.parse(xml).getDocumentElement();
        } catch (ParserConfigurationException | SAXException e) {
            throw new IOException(e.getMessage());
        }
    }
    
}