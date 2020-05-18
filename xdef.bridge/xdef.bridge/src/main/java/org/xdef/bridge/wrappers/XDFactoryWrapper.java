package org.xdef.bridge.wrappers;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.net.URL;
import java.util.Properties;

import javax.imageio.IIOException;

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
import org.xdef.sys.ReportWriter;

public class XDFactoryWrapper extends RemoteHandlingObject {

    private static final int FUNCTION_COMPILEXD = 1;

    private static final int OVERLOAD_COMPILEXD_FILES = 1;
    private static final int OVERLOAD_COMPILEXD_STREAMS = 2;
    private static final int OVERLOAD_COMPILEXD_STRINGS = 3;
    private static final int OVERLOAD_COMPILEXD_URLS = 4;
    private static final int OVERLOAD_COMPILEXD_OBJECTS = 5;
    private static final int OVERLOAD_COMPILEXD_OBJECTS_IDS = 6;

    public XDFactoryWrapper(Client client) {
        super(client);
    }

    private Properties readProperties(BinaryDataReader reader) {
        try {
            Properties props = new Properties();
            int propCount = reader.readInt();
            if (propCount == 0) return null;
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
                case OVERLOAD_COMPILEXD_OBJECTS:
                    pool = compileXDObject(props, reader);
                    break;
                case OVERLOAD_COMPILEXD_OBJECTS_IDS:
                    pool = compileXDObjectIds(props, reader);
                    break;
                default:
                    throw new Exception("Unknown funciton overload.");
            }
            
            if (pool == null) throw new Exception("CompileXD returned null");
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
    
    private XDPool compileXDObject(Properties props, BinaryDataReader reader) throws IOException {
        Object[] args = readObjectArgs(reader);
        return XDFactory.compileXD(props, args);
    }
    
    private XDPool compileXDObjectReporter(Properties props, BinaryDataReader reader) throws IOException {
        int readerId = reader.readInt();
        Object[] args = readObjectArgs(reader);
        ReportWriter reportWriter = (ReportWriter) client.getLocalObject(readerId);
        return XDFactory.compileXD(reportWriter, props, args);
        
    }
    
    private XDPool compileXDObjectIds(Properties props, BinaryDataReader reader) throws IOException {
        Object[] args = readObjectArgs(reader);
        int sourceIdCount = reader.readInt();
        String[] sourceIds = new String[sourceIdCount];
        for (int i = 0; i < sourceIdCount; i++) {
            sourceIds[i] = reader.readSharpString();
        }
        return XDFactory.compileXD(props, args, sourceIds);
    }

    private Object[] readObjectArgs(BinaryDataReader reader) throws IOException {
        int paramCount = reader.readInt();
        Object[] res = new Object[paramCount];
        for(int i = 0; i < paramCount; i++) {
            int objectType = reader.readInt();
            switch (objectType) {
                case OVERLOAD_COMPILEXD_FILES:
                    res[i] = new File(reader.readSharpString());
                    break;
                case OVERLOAD_COMPILEXD_STREAMS:
                    res[i] = new RemoteInputStream(new RemoteStreamWrapper(client, reader.readInt()));
                    break;
                case OVERLOAD_COMPILEXD_STRINGS:
                    res[i] = reader.readSharpString();
                    break;
                case OVERLOAD_COMPILEXD_URLS:
                    res[i] = new URL(reader.readSharpString());
                    break;
                default:
                    throw new IIOException("Invalid parameter type.");
            }
        }
        return res;
    }
    
    
}
