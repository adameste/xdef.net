package org.xdef.bridge.wrappers;

import java.io.File;
import java.io.IOException;
import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.util.ByteArrayBuilder;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;

import org.xdef.XDFactory;
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
        var mapper = new ObjectMapper();
        JsonNode tree;
        try {
            tree = mapper.readTree(json);
            var props = new Properties();
            var fields = tree.fields();
            while (fields.hasNext()) {
                var elem = fields.next();
                props.setProperty(elem.getKey(), elem.getValue().textValue());
            }
            return props;
        } catch (JsonProcessingException e) {
            return null;
        }
    }

    @Override
    public Request handleRequest(Request request) {
        var reader = request.getReader();
        switch (request.getFunction()) {
            case FUNCTION_COMPILE_XD_1:
                return compileXD1(reader);
            default:
                return null;
        }
    }

    private Request compileXD1(BinaryDataReader reader) {
        var props = readProperties(reader);
        try {
            List<File> files = new ArrayList<>();
            var fileCount = reader.readInt();
            for (int i = 0; i < fileCount; i++) {
                var path = reader.readSharpString();
                files.add(new File(path));
            }
            var pool = XDFactory.compileXD(props, files.toArray(new File[0]));
            var wrapper = new XDPoolWrapper(client, pool);
            client.registerRemoteObject(wrapper);
            return new Request(0, new BinaryDataBuilder().add(wrapper.getObjectId()).build());
        } catch (IOException e) {
            return null;
        }
    }

}