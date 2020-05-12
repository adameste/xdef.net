using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using xdef.net.Utils;

namespace xdef.net.Connection
{
    public class Request
    {
        public int ObjectId { get; set; }
        public int Function { get; set; }
        public int ClientRequestId { get; set; }
        public int ServerRequestId { get; set; }
        public byte[] Data { get; set; }

        public Request(int function, byte[] data, int objectId = 0)
        {
            Function = function;
            Data = data;
            ObjectId = objectId;
        }

        public Request() { }

        public void WriteToStream(Stream stream)
        {
            using (var writer = new BigEndianBinaryWriter(stream, Encoding.UTF8, true))
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
            using (var reader = new BigEndianBinaryReader(stream, Encoding.UTF8, true))
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

        public BigEndianBinaryReader Reader => new BigEndianBinaryReader(new MemoryStream(Data));
    }
}
