package org.xdef.bridge.utils;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.StringWriter;
import java.nio.charset.StandardCharsets;
import java.util.Properties;
import java.util.Set;

import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;

import org.w3c.dom.Document;
import org.w3c.dom.Node;

public class BinaryDataBuilder {
    private ByteArrayOutputStream stream;
    private DataOutputStream dataStream;

    public BinaryDataBuilder() {
        stream = new ByteArrayOutputStream();
        dataStream = new DataOutputStream(stream);
    }

    public BinaryDataBuilder add(int x) {
        try {
            dataStream.writeInt(x);
        } catch (IOException e) {
        }
        return this;
    }

    public BinaryDataBuilder add(String x) {

        try {
            if (x == null)
                dataStream.writeInt(0);
            else {
                byte[] data = x.getBytes(StandardCharsets.UTF_8);
                dataStream.writeInt(data.length);
                dataStream.write(data);
            }
        } catch (IOException e) {
        }
        return this;
    }

    public BinaryDataBuilder add(byte[] buf, int offset, int len) {
        try {
            dataStream.write(buf, offset, len);
        } catch (IOException ex) {
            // Do nothing
        }
        return this;
    }

    public BinaryDataBuilder add(boolean x) {
        try {
            dataStream.writeBoolean(x);
        } catch (IOException ex) {
            // Do nothing
        }
        return this;
    }

    public BinaryDataBuilder add(String[] x) {
        try {
            dataStream.writeInt(x.length);
            for (String it : x)
                this.add(it);
        } catch (IOException e) {
        }
        return this;
    }

    public BinaryDataBuilder add(Properties props) {
        Set<String> names = props.stringPropertyNames();
        add(names.size());
        for (String name : names) {
            add(name).add(props.getProperty(name));
        }
        return this;
    }

    public BinaryDataBuilder add(Node doc) {
        TransformerFactory transformerFactory = TransformerFactory.newInstance();
        Transformer transformer;
        try {
            transformer = transformerFactory.newTransformer();
            DOMSource source = new DOMSource(doc);
            StreamResult result = new StreamResult(new StringWriter());
            transformer.transform(source, result);
            String strObject = result.getWriter().toString();
            add(strObject);
        } catch (TransformerException e) {

        }
        return this;
    }

    public byte[] build() {
        try {
            dataStream.flush();
        } catch (IOException e) {
        }
        return stream.toByteArray();
    }

}