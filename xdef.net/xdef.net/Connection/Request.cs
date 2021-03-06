﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using xdef.net.Utils;
using xdef.net.Utils;

namespace xdef.net.Connection
{
    internal class Request
    {
        public int ObjectId { get; set; }
        public int Function { get; set; }
        public int ClientRequestId { get; set; }
        public int ServerRequestId { get; set; }
        public byte[] Data { get; set; }

        internal Request(int function, byte[] data, int objectId = 0)
        {
            Function = function;
            Data = data;
            ObjectId = objectId;
        }

        public Request() { }

        internal void WriteToStream(Stream stream)
        {
            using (var writer = new BigEndianBinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(ObjectId);
                writer.Write(Function);
                writer.Write(ClientRequestId);
                writer.Write(ServerRequestId);
                writer.Write(Data == null ? 0 : Data.Length);
                if (Data != null)
                    writer.Write(Data);
            }
        }
        internal async Task WriteToStreamAsync(Stream stream)
        {
            var str = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(str, Encoding.UTF8, true))
            {
                writer.Write(ObjectId);
                writer.Write(Function);
                writer.Write(ClientRequestId);
                writer.Write(ServerRequestId);
                writer.Write(Data == null ? 0 : Data.Length);
                if (Data != null)
                    writer.Write(Data);
                await str.CopyToAsync(stream);
            }
        }

        internal static Request ReadFromStream(Stream stream)
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

        internal BigEndianBinaryReader Reader => new BigEndianBinaryReader(new MemoryStream(Data), Encoding.UTF8);
        internal int ResultObjectId => BigEndianBitConverter.ToInt32(Data, 0);
    }
}
