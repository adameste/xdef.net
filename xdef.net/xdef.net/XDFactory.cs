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
    public sealed class XDFactory : RemoteObject
    {
        private const int FUNCTION_COMPILEXD = 1;

        private const int OVERLOAD_COMPILEXD_FILES = 1;
        private const int OVERLOAD_COMPILEXD_STREAMS = 2;
        private const int OVERLOAD_COMPILEXD_STRINGS = 3;
        private const int OVERLOAD_COMPILEXD_URLS = 4;
        private const int OVERLOAD_COMPILEXD_OBJECTS = 5;
        private const int OVERLOAD_COMPILEXD_OBJECTS_IDS = 6;

        public XDFactory(int objectId, Client client) : base(objectId, client)
        {
        }


        public XDPool CompileXD(xdef.net.Utils.Properties props, params FilePath[] sourceFiles)
        {
            Request req = null;
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream, Encoding.UTF8))
            {
                writer.Write(OVERLOAD_COMPILEXD_FILES);
                writer.Write(props);
                writer.Write(sourceFiles.Length);
                foreach (var it in sourceFiles)
                {
                    if (!it.Exists) throw new FileNotFoundException();
                    writer.Write(it.JavaPath);
                }
                writer.Flush();
                req = new Request(FUNCTION_COMPILEXD, stream.ToArray(), ObjectId);
            }
            return GetXDPoolFromRequest(req);
        }

        public XDPool CompileXD(xdef.net.Utils.Properties props, params Stream[] sourceStreams)
        {
            if (sourceStreams.Any(p => !p.CanRead))
                throw new ArgumentException("All streams must be readable.");
            Request req = null;
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream, Encoding.UTF8))
            {
                writer.Write(OVERLOAD_COMPILEXD_STREAMS);
                writer.Write(props);
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
            return GetXDPoolFromRequest(req);
        }

        public XDPool CompileXD(xdef.net.Utils.Properties props, params string[] sources)
        {
            Request req = null;
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream, Encoding.UTF8))
            {
                writer.Write(OVERLOAD_COMPILEXD_STRINGS);
                writer.Write(props);
                writer.Write(sources.Length);
                foreach (var it in sources)
                {
                    writer.Write(it);
                }
                writer.Flush();
                req = new Request(FUNCTION_COMPILEXD, stream.ToArray(), ObjectId);
            }
            return GetXDPoolFromRequest(req);
        }

        private XDPool GetXDPoolFromRequest(Request req)
        {
            var response = SendRequestWithResponse(req);
            return new XDPool(BigEndianBitConverter.ToInt32(response.Data, 0), _client);
        }

        public XDPool CompileXD(xdef.net.Utils.Properties props, params Uri[] sources)
        {
            Request req = null;
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream, Encoding.UTF8))
            {
                writer.Write(OVERLOAD_COMPILEXD_URLS);
                writer.Write(props);
                writer.Write(sources.Length);
                foreach (var it in sources)
                {
                    writer.Write(it.AbsoluteUri);
                }
                writer.Flush();
                req = new Request(FUNCTION_COMPILEXD, stream.ToArray(), ObjectId);
            }
            return GetXDPoolFromRequest(req);
        }

        public XDPool CompileXD(xdef.net.Utils.Properties props, params object[] sources)
        {
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream, Encoding.UTF8))
            {
                writer.Write(OVERLOAD_COMPILEXD_OBJECTS);
                writer.Write(props);
                SerializeObjectSources(writer, sources);
                writer.Flush();
                return GetXDPoolFromRequest(new Request(FUNCTION_COMPILEXD, stream.ToArray(), ObjectId));
            }
            
        }

        public XDPool CompileXD(xdef.net.Utils.Properties props, IEnumerable<object> sources, IEnumerable<string> sourceIds)
        {
            if (sources.Count() != sourceIds.Count()) throw new ArgumentException("Source count is not the same as source id count.");
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream, Encoding.UTF8))
            {
                writer.Write(OVERLOAD_COMPILEXD_OBJECTS_IDS);
                writer.Write(props);
                SerializeObjectSources(writer, sources);
                writer.Write(sourceIds.Count());
                foreach (var it in sourceIds)
                    writer.Write(it);
                writer.Flush();
                return GetXDPoolFromRequest(new Request(FUNCTION_COMPILEXD, stream.ToArray(), ObjectId));
            }
        }

        private void SerializeObjectSources(BigEndianBinaryWriter writer, IEnumerable<object> sources)
        {
            var registeredObjects = new List<RemoteHandlingObject>();
            foreach (var it in sources)
            {
                if (it is string s)
                {
                    writer.Write(OVERLOAD_COMPILEXD_STRINGS);
                    writer.Write(s);
                }
                else if (it is Uri u)
                {
                    writer.Write(OVERLOAD_COMPILEXD_URLS);
                    writer.Write(u.AbsoluteUri);
                }
                else if (it is FilePath fp)
                {
                    writer.Write(OVERLOAD_COMPILEXD_FILES);
                    writer.Write(fp.JavaPath);
                }
                else if (it is Stream stream)
                {
                    writer.Write(OVERLOAD_COMPILEXD_FILES);
                    var wrap = new RemoteStreamWrapper(_client, stream);
                    writer.Write(_client.RegisterObject(wrap));
                    registeredObjects.Add(wrap);
                }
                else
                {
                    foreach (var jt in registeredObjects)
                        _client.DeleteLocalObject(jt.ObjectId);
                    if (it == null)
                        throw new ArgumentNullException();
                    else
                        throw new ArgumentException($"Invalid argument of type: {it.GetType().FullName}");
                }
            }
        }
    }
}
