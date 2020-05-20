package org.xdef.bridge.utils;

import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;
import java.util.Properties;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
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

    public Document readDocument() throws IOException {
        String xml = this.readSharpString();
        try {
            DocumentBuilder documentBuilder = DocumentBuilderFactory.newInstance().newDocumentBuilder();
            return documentBuilder.parse(xml);
        } catch (ParserConfigurationException | SAXException e) {
            throw new IOException(e.getMessage());
        }
    }

    public Properties readProperties() {
        try {
            final Properties props = new Properties();
            final int propCount = this.readInt();
            if (propCount == 0)
                return null;
            for (int i = 0; i < propCount; i++) {
                final String key = this.readSharpString();
                final String value = this.readSharpString();
                props.setProperty(key, value);
            }
            return props;
        } catch (final IOException ex) {
            return null;
        }
    }
    
}