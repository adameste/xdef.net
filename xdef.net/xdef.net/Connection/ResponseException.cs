﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using xdef.net.Utils;

namespace xdef.net.Connection
{

    class ResponseException : Response
    {
        private const int RESPONSE_FUNCTION_EXCEPTION = 2;

        public ResponseException(int errCode, string message) : base(null)
        {
            Function = RESPONSE_FUNCTION_EXCEPTION;
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream))
            {
                writer.Write(errCode);
                writer.Write(message);
                Data = stream.ToArray();
            }
        }
    }
}
