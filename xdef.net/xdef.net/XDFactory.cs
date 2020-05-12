﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using xdef.net.Connection;
using xdef.net.Connection.Library;
using xdef.net.Utils;

namespace xdef.net
{
    public class XDFactory : RemoteObject
    {
        private const int FUNCTION_COMPILEXD = 1;

        private const int OVERLOAD_COMPILEXD_FILES = 1;
        private const int OVERLOAD_COMPILEXD_STREAMS = 2;

        public XDFactory(int objectId, Client client) : base(objectId, client)
        {
        }

        private void SerializeProperties(Properties properties, BigEndianBinaryWriter writer)
        {
            if (properties == null)
            {
                writer.Write(0);
            }
            else
            {
                var json = JsonConvert.SerializeObject(properties);
                writer.Write(json);
            }
        }

        public XDPool CompileXD(Properties props, params FilePath[] sourceFiles)
        {
            Request req = null;
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream, Encoding.UTF8))
            {
                writer.Write(OVERLOAD_COMPILEXD_FILES);
                SerializeProperties(props, writer);
                writer.Write(sourceFiles.Length);
                foreach (var it in sourceFiles)
                {
                    if (!it.Exists) throw new FileNotFoundException();
                    writer.Write(it.JavaPath);
                }
                writer.Flush();
                req = new Request(FUNCTION_COMPILEXD, stream.ToArray(), ObjectId);
            }
            var response = SendRequestWithResponse(req);
            return new XDPool(BigEndianBitConverter.ToInt32(response.Data, 0), _client);
        }

        public XDPool CompileXD(Properties props, params Stream[] sourceStreams)
        {
            if (sourceStreams.Any(p => !p.CanRead))
                throw new ArgumentException("All streams must be readable.");
            Request req = null;
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream, Encoding.UTF8))
            {
                writer.Write(OVERLOAD_COMPILEXD_STREAMS);
                SerializeProperties(props, writer);
                writer.Write(sourceStreams.Length);
                foreach (var sourceStream in sourceStreams)
                {
                    var remoteStream = new RemoteStreamWrapper(_client, sourceStream);
                    var streamId = _client.RegisterObject(remoteStream);
                    writer.Write(streamId);
                }
                writer.Flush();
                req = new Request(FUNCTION_COMPILEXD, stream.ToArray(), ObjectId);
            }
            var response = SendRequestWithResponse(req);
            return new XDPool(BigEndianBitConverter.ToInt32(response.Data, 0), _client);
        }
    }
}