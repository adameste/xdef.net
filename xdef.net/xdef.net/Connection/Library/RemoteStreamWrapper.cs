﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using xdef.net.Utils;

namespace xdef.net.Connection.Library
{
    class RemoteStreamWrapper : RemoteHandlingObject, IDisposable
    {
        private const int FUNCTION_AVAILABLE = 1;
        private const int FUNCTION_CLOSE = 2;
        private const int FUNCTION_READ = 3;
        private const int FUNCTION_WRITE = 4;

        private readonly Stream _stream;

        public RemoteStreamWrapper(int objectId, Client client, Stream stream) : base(objectId, client)
        {
            _stream = stream;
        }

        public override Request HandleRequest(Request request)
        {
            switch (request.Function)
            {
                case FUNCTION_AVAILABLE:
                    return Available();
                case FUNCTION_CLOSE:
                    return Close();
                case FUNCTION_READ:
                    return Read(request);
                case FUNCTION_WRITE:
                    return Write(request);
                default:
                    return new ResponseException(Response.ERROR_CODE_UNKNOWN_FUNCTION, $"Unknown function {request.Function} on {nameof(RemoteStreamWrapper)}");
            }
        }

        private Request Write(Request request)
        {
            using (var reader = request.Reader)
            {
                var len = reader.ReadInt32();
                var buf = reader.ReadBytes(len);
                _stream.Write(buf, 0, len);
            }
            return null;
        }

        private Request Read(Request request)
        {
            using (var reader = request.Reader)
            {
                var len = reader.ReadInt32();
                var buf = new byte[len];
                var bytesRead = _stream.Read(buf, 0, len);
                var stream = new MemoryStream();
                using (var writer = new BigEndianBinaryWriter(stream))
                {
                    writer.Write(bytesRead);
                    writer.Write(buf, 0, bytesRead);
                    return new Response(stream.ToArray());
                }
            }
        }

        private Request Close()
        {
            _stream.Close();
            return null;
        }

        private Request Available()
        {
            var avail = (int)(_stream.Length - _stream.Position);
            return new Response(BigEndianBitConverter.GetBytes(avail));
        }

        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}
