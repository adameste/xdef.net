using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using xdef.net.Connection;
using xdef.net.Sys;
using xdef.net.Utils;

namespace xdef.net
{
    public sealed class XDDocument : RemoteObject
    {
        private const int FUNCTION_XPARSE = 1;


        private const int XPARSE_OVERLOAD_FILE = 1;
        private const int XPARSE_OVERLOAD_STRING = 2;
        private const int XPARSE_OVERLOAD_URL = 3;
        private const int XPARSE_OVERLOAD_STREAM = 4;
        private const int XPARSE_OVERLOAD_NODE = 5;

        public XDDocument(int objectId, Client client) : base(objectId, client)
        {
        }

        public XElement XParse(FilePath file, ReportWriter reportWriter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                var res = SendRequestWithResponse(new Request(FUNCTION_XPARSE,
                    builder.Add(XPARSE_OVERLOAD_FILE).Add(file.JavaPath).Add(reportWriter?.ObjectId ?? 0).Build(),
                    ObjectId));
                return ParseElementFromResponse(res);
            }
        }

        private XElement ParseElementFromResponse(Request res)
        {
            using (var reader = res.Reader)
            {
                return XElement.Parse(reader.ReadString());
            }
        }
    }
}
