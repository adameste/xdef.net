using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Utils;

namespace xdef.net.Connection
{
    internal class DeleteObjectRequest : Request
    {
        private const int FUNCTION_DELETE_OBJECT = 2;

        public DeleteObjectRequest(int objectId)
        {
            Function = FUNCTION_DELETE_OBJECT;
            Data = BigEndianBitConverter.GetBytes(objectId);
        }
    }
}
