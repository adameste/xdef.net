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
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.utils.BinaryDataBuilder;
import org.xdef.bridge.utils.BinaryDataReader;

public class XDFactoryWrapper extends RemoteHandlingObject {

    private static final int FUNCTION_COMPILEXD = 1;

    private static final int OVERLOAD_COMPILEXD_FILES = 1;

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
    public Response handleRequest(Request request) {
        BinaryDataReader reader = request.getReader();
        switch (request.getFunction()) {
            case FUNCTION_COMPILEXD:
                return compileXD(request, reader);
            default:
                return null;
        }
    }

    private Response compileXD(Request request, BinaryDataReader reader) {
        try {
            int overload = reader.readInt();
            Properties props = readProperties(reader);
            XDPool pool = null;
            switch(overload) {
                case OVERLOAD_COMPILEXD_FILES:
                    pool = compileXDFiles(props, reader);
                break;
                default: break;
            }
            XDPoolWrapper wrapper = new XDPoolWrapper(client, pool);
            client.registerRemoteObject(wrapper);
            return new Response(new BinaryDataBuilder().add(wrapper.getObjectId()).build());
        } catch (IOException e) {
            return null;
        }
    }

    private XDPool compileXDFiles(Properties props, BinaryDataReader reader) throws IOException{
        List<File> files = new ArrayList<>();
            int fileCount = reader.readInt();
            for (int i = 0; i < fileCount; i++) {
                String path = reader.readSharpString();
                files.add(new File(path));
            }
            return XDFactory.compileXD(props, files.toArray(new File[0]));
    }

}