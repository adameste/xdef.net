package org.xdef.bridge.Server.Requests;

import java.io.DataOutputStream;
import java.io.IOException;

public interface StreamWritable {
    public void writeToStream(DataOutputStream stream) throws IOException;
}