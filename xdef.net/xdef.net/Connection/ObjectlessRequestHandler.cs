using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net.Connection
{
    class ObjectlessRequestHandler
    {
        public const int FUNCTION_CREATE_OBJECT = 1;
        public const int FUNCTION_DELETE_OBJECT = 2;

        private Client _client;

        public ObjectlessRequestHandler(Client client)
        {
            _client = client;
        }

        internal Response HandleRequest(Request request)
        {
            switch (request.Function)
            {
                case FUNCTION_CREATE_OBJECT:
                    return null;
                case FUNCTION_DELETE_OBJECT:
                    using (var reader = request.Reader)
                    {
                        _client.DeleteLocalObject(reader.ReadInt32());
                    }
                    return null;
                default: return null;
            }
        }
    }
}
