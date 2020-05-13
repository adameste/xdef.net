package org.xdef.bridge.wrappers;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;
import java.io.InputStream;
import java.net.URL;
import org.xdef.XDFactory;
import org.xdef.XDPool;
import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.Client;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.server.requests.ResponseException;
import org.xdef.bridge.utils.BinaryDataBuilder;
import org.xdef.bridge.utils.BinaryDataReader;
import org.xdef.bridge.wrappers.streams.RemoteInputStream;
import org.xdef.bridge.wrappers.streams.RemoteStreamWrapper;

public class XDFactoryWrapper extends RemoteHandlingObject {

    private static final int FUNCTION_COMPILEXD = 1;

    private static final int OVERLOAD_COMPILEXD_FILES = 1;
    private static final int OVERLOAD_COMPILEXD_STREAMS = 2;
    private static final int OVERLOAD_COMPILEXD_STRINGS = 3;
    private static final int OVERLOAD_COMPILEXD_URLS = 4;

    public XDFactoryWrapper(Client client) {
        super(client);
    }

    private Properties readProperties(BinaryDataReader reader) {
        try {
            Properties props = new Properties();
            int propCount = reader.readInt();
            for (int i = 0; i < propCount; i++) {
                String key = reader.readSharpString();
                String value = reader.readSharpString();
                props.setProperty(key, value);
            }
            return props;
        } catch (IOException ex) {
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
            switch (overload) {
                case OVERLOAD_COMPILEXD_FILES:
                    pool = compileXDFiles(props, reader);
                    break;
                case OVERLOAD_COMPILEXD_STREAMS:
                    pool = compileXDStreams(props, reader);
                    break;
                case OVERLOAD_COMPILEXD_STRINGS:
                    pool = compileXDStrings(props, reader);
                    break;
                case OVERLOAD_COMPILEXD_URLS:
                    pool = compileXDUrls(props, reader);
                    break;
                default:
                    break;
            }
            XDPoolWrapper wrapper = new XDPoolWrapper(client, pool);
            client.registerRemoteObject(wrapper);
            return new Response(new BinaryDataBuilder().add(wrapper.getObjectId()).build());
        }  catch (Exception ex)
        {
            return new ResponseException(ResponseException.ERROR_CODE_INVALID_REQUEST,
                    "XDFactory: failed to create XDPool: " + ex.getMessage());
        }
    }

    private XDPool compileXDFiles(Properties props, BinaryDataReader reader) throws IOException {
        int fileCount = reader.readInt();
        File[] files = new File[fileCount];
        for (int i = 0; i < fileCount; i++) {
            String path = reader.readSharpString();
            files[i] = new File(path);
        }
        return XDFactory.compileXD(props, files);
    }

    private XDPool compileXDStreams(Properties props, BinaryDataReader reader) throws IOException {
        int streamCount = reader.readInt();
        InputStream[] streams = new InputStream[streamCount];
        for (int i = 0; i < streamCount; i++) {
            int remoteObjectId = reader.readInt();
            RemoteStreamWrapper wrapper = new RemoteStreamWrapper(client, remoteObjectId);
            streams[i] = new RemoteInputStream(wrapper);
        }
        return XDFactory.compileXD(props, streams);
    }

    private XDPool compileXDStrings(Properties props, BinaryDataReader reader) throws IOException {
        int stringCount = reader.readInt();
        String[] strings = new String[stringCount];
        for (int i = 0; i < stringCount; i++) {
            strings[i] = reader.readSharpString();
        }
        return XDFactory.compileXD(props, strings);
    }

    private XDPool compileXDUrls(Properties props, BinaryDataReader reader) throws IOException {
        int urlCount = reader.readInt();
        URL[] urls = new URL[urlCount];
        for (int i = 0; i < urlCount; i++) {
            urls[i] = new URL(reader.readSharpString());
        }
        return XDFactory.compileXD(props, urls);
    }
    
    
}
