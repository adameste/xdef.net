using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using xdef.net.Connection;
using xdef.net.Utils;

namespace xdef.net
{
    public class XDFactory : RemoteObject
    {
        private const int FUNCTION_COMPILEXD_1 = 1;

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
            var req = new Request()
            {
                ObjectId = _objectId,
                Function = FUNCTION_COMPILEXD_1
            };
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream, Encoding.UTF8))
            {
                SerializeProperties(props, writer);
                writer.Write(sourceFiles.Length);
                foreach (var it in sourceFiles)
                {
                    if (!it.Exists) throw new FileNotFoundException();
                    writer.Write(it.JavaPath);
                }
                writer.Flush();
                req.Data = stream.ToArray();
            }
            var response = SendRequestWithResponse(req);
            return new XDPool(BigEndianBitConverter.ToInt32(response.Data, 0), _client);
        }
    }
}
