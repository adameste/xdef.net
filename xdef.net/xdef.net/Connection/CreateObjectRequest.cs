using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net.Connection
{
    internal class CreateObjectRequest : Request
    {
        private const int FUNCTION_CREATE_OBJECT = 1;

        public const string OBJECT_XDFACTORY = "XDFactory";

        public CreateObjectRequest(string objectName)
        {
            Function = FUNCTION_CREATE_OBJECT;
            Data = Encoding.UTF8.GetBytes(objectName);
        }
    }
}
