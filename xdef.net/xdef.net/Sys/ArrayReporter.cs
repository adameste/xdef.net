using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using xdef.net.Connection;

namespace xdef.net.Sys
{
    public sealed class ArrayReporter : Reporter
    {
        private int _v;

        public ArrayReporter() 
        {
            var response = _client.SendRequestWithResponse(new CreateObjectRequest("ArrayReporter"));
            using (var reader = response.Reader)
                ObjectId = reader.ReadInt32();
            _reportReaderWrapper = new ReportReaderWrapper(_client, ObjectId);
            _reportWriterWrapper = new ReportWriterWrapper(_client, ObjectId);
        }

        internal ArrayReporter(int objectId, Client client)
        {
            ObjectId = objectId;
            _client = client;
        }
    }
}
