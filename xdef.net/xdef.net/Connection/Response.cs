using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net.Connection
{
    class Response : Request
    {
        private const int RESPONSE_FUNCTION_OK = 1;

        public const int ERROR_CODE_UNKNOWN_OBJECT = 1;
        public const int ERROR_CODE_UNKNOWN_FUNCTION = 2;

        public Response(byte[] data) : base(RESPONSE_FUNCTION_OK, data, 0)
        {
        }
    }
}
