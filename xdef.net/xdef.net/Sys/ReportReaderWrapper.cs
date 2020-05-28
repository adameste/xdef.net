using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using xdef.net.Connection;
using xdef.net.Connection.Library;
using xdef.net.Utils;

namespace xdef.net.Sys
{
    public class ReportReaderWrapper : ReportReader
    {
        private readonly Client _client;
        private readonly int _objectId;


        private const int FUNCTION_READER_REPORT = 1;
        private const int FUNCTION_READER_CLOSE = 2;
        private const int FUNCTION_READER_PRINT_REPORTS = 3;
        private const int FUNCTION_READER_PRINT_TO_STRING = 4;
        private const int FUNCTION_READER_WRITE_REPORTS = 5;

        internal ReportReaderWrapper(Client client, int objectId)
        {
            _client = client;
            _objectId = objectId;
        }





        public Report Report
        {
            get
            {
                var response = _client.SendRequestWithResponse(new Request(FUNCTION_READER_REPORT, null, _objectId));
                using (var reader = response.Reader)
                {
                    var objId = reader.ReadInt32();
                    if (objId == 0) return null;
                    return new Report(objId, _client);
                }
            }
        }

        public int ObjectId => _objectId;

        public void Close()
        {
            _client.SendRequestWithoutResponse(new Request(FUNCTION_READER_CLOSE, null, _objectId));
        }

        public void PrintReports(Stream output)
        {
            PrintReports(output, null);
        }

        public void PrintReports(Stream output, string language)
        {
            if (!output.CanWrite) throw new ArgumentException("Stream is not writable.");
            var wrapper = new RemoteStreamWrapper(_client, output);
            var id = _client.RegisterLocalObject(wrapper);
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream))
            {
                writer.Write(wrapper.ObjectId);
                writer.Write(language);
                writer.Flush();
                _client.SendRequestWithResponse(new Request(FUNCTION_READER_PRINT_REPORTS, stream.ToArray(), _objectId));
            }
        }

        public string PrintToString()
        {
            return PrintToString(null);
        }

        public string PrintToString(string language)
        {
            var stream = new MemoryStream();
            using (var writer = new BigEndianBinaryWriter(stream))
            {
                writer.Write(language);
                writer.Flush();
                var response = _client.SendRequestWithResponse(new Request(FUNCTION_READER_PRINT_TO_STRING, stream.ToArray(), _objectId));
                using (var reader = response.Reader)
                    return reader.ReadString();
            }
        }

        public void WriteReports(ReportWriter reporter)
        {
            if (!(reporter is RemoteObject ro))
            {
                throw new ArgumentException("Can only use with remote objects.");
            }
            _client.SendRequestWithResponse(new Request(FUNCTION_READER_WRITE_REPORTS, BigEndianBitConverter.GetBytes(ro.ObjectId), _objectId));
        }

        void ReportReader.Close()
        {
            _client.SendRequestWithResponse(new Request(FUNCTION_READER_CLOSE, null, _objectId));
        }

        void ReportReader.PrintReports(Stream output)
        {
            PrintReports(output, null);
        }

        void ReportReader.PrintReports(Stream output, string language)
        {
            if (!output.CanWrite) throw new ArgumentException("Stream not writable.");
            var wrap = new RemoteStreamWrapper(_client, output);
            var id = _client.RegisterLocalObject(wrap);
            using (var builder = new BigEndianDataBuilder())
            {
                _client.SendRequestWithResponse(new Request(FUNCTION_READER_PRINT_REPORTS, builder.Add(id).Add(language).Build(), _objectId));
            }
        }

        string ReportReader.PrintToString()
        {
            return PrintToString(null);
        }

        string ReportReader.PrintToString(string language)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                var res = _client.SendRequestWithResponse(new Request(FUNCTION_READER_PRINT_TO_STRING, builder.Add(language).Build(), _objectId));
                using (var reader = res.Reader)
                    return reader.ReadString();
            }
        }

        void ReportReader.WriteReports(ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                var res = _client.SendRequestWithResponse(new Request(FUNCTION_READER_WRITE_REPORTS, builder.Add(reporter.ObjectId).Build(), _objectId));
            }
        }
    }
}
