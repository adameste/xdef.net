using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xdef.net.Connection
{
    public class Request
    {
        public int ObjectId { get; set; }
        public int Function { get; set; }
        public int ClientRequestId { get; set; }
        public int ServerRequestId { get; set; }
        public byte[] Data { get; set; }

        public void WriteToStream(Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(ObjectId);
                writer.Write(Function);
                writer.Write(ClientRequestId);
                writer.Write(ServerRequestId);
                writer.Write(Data == null ? 0 : Data.Length);
                writer.Write(Data);
            }
        }

        public static Request ReadFromStream(Stream stream)
        {
            using (var reader = new BinaryReader(stream))
            {
                return new Request()
                {
                    ObjectId = reader.ReadInt32(),
                    Function = reader.ReadInt32(),
                    ClientRequestId = reader.ReadInt32(),
                    ServerRequestId = reader.ReadInt32(),
                    Data = reader.ReadBytes(reader.ReadInt32())
                };
            }
        }
    }
}
