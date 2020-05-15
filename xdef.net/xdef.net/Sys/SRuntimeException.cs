using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net.Sys
{
    class SRuntimeException : Exception
    {
        public SRuntimeException(string message) : base(message)
        {
        }

        public SRuntimeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
