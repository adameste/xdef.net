using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using xdef.net.Connection;
using xdef.net.Sys;
using xdef.net.Utils;

namespace xdef.net
{
    public sealed class XDPool : RemoteObject
    {
        private const int FUNCTION_CREATE_XDDOCUMENT = 1;



        public XDPool(int objectId, Client client) : base(objectId, client)
        {
            
        }

        public XDDocument CreateXDDocument()
        {
            return CreateXDDocument(null);
        }

        public XDDocument CreateXDDocument(string name)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATE_XDDOCUMENT,
                    builder.Add(name).Build(),
                    ObjectId));
                return CreateXDDocumentFromResult(res);
            }
        }

        private XDDocument CreateXDDocumentFromResult(Request res)
        {
            var id = BigEndianBitConverter.ToInt32(res.Data, 0);
            return new XDDocument(id, _client);
        }
    }
}
