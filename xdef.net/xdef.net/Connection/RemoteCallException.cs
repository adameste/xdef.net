using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net.Connection
{
    public class RemoteCallException : Exception
    {
        public int ErrorCode { get; }

        public RemoteCallException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public RemoteCallException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
