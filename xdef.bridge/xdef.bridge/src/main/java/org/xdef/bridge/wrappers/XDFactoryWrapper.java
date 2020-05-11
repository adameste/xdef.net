package org.xdef.bridge.wrappers;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Properties;
import java.util.Map.Entry;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;

import org.xdef.XDFactory;
import org.xdef.XDPool;
import org.xdef.bridge.remoteObjects.RemoteObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.utils.BinaryDataBuilder;
import org.xdef.bridge.utils.BinaryDataReader;

public class XDFactoryWrapper extends RemoteObject {

    private static final int FUNCTION_COMPILE_XD_1 = 1;

    public XDFactoryWrapper(Client client) {
        super(client);
    }

    private Properties readProperties(BinaryDataReader reader) {
        String json = null;
        try {
            json = reader.readSharpString();
        } catch (IOException e) {
        }
        if (json == null)
            return null;
            ObjectMapper mapper = new ObjectMapper();
        JsonNode tree;
        try {
            tree = mapper.readTree(json);
            Properties props = new Properties();
            Iterator<Entry<String, JsonNode>> fields = tree.fields();
            while (fields.hasNext()) {
                Entry<String, JsonNode> elem = fields.next();
                props.setProperty(elem.getKey(), elem.getValue().textValue());
            }
            return props;
        } catch (JsonProcessingException e) {
            return null;
        }
    }

    @Override
    public Request handleRequest(Request request) {
        BinaryDataReader reader = request.getReader();
        switch (request.getFunction()) {
            case FUNCTION_COMPILE_XD_1:
                return compileXD1(reader);
            default:
                return null;
        }
    }

    private Request compileXD1(BinaryDataReader reader) {
        Properties props = readProperties(reader);
        try {
            List<File> files = new ArrayList<>();
            int fileCount = reader.readInt();
            for (int i = 0; i < fileCount; i++) {
                String path = reader.readSharpString();
                files.add(new File(path));
            }
            XDPool pool = XDFactory.compileXD(props, files.toArray(new File[0]));
            XDPoolWrapper wrapper = new XDPoolWrapper(client, pool);
            client.registerRemoteObject(wrapper);
            return new Request(0, new BinaryDataBuilder().add(wrapper.getObjectId()).build());
        } catch (IOException e) {
            return null;
        }
    }

}