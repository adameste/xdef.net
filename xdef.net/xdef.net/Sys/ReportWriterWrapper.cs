using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Connection;
using xdef.net.Utils;

namespace xdef.net.Sys
{
    internal sealed class ReportWriterWrapper : ReportWriter
    {
        private readonly Client _client;
        private readonly int _objectId;


        private const int FUNCTION_WRITER_LANGUAGE = 1001;
        private const int FUNCTION_WRITER_LAST_ERROR_REPORT = 1002;
        private const int FUNCTION_WRITER_SIZE = 1003;
        private const int FUNCTION_WRITER_FATALS = 1004;
        private const int FUNCTION_WRITER_ERRORS = 1005;
        private const int FUNCTION_WRITER_ERRORWARNINGS = 1006;
        private const int FUNCTION_WRITER_FATAL_COUNT = 1007;
        private const int FUNCTION_WRITER_ERROR_COUNT = 1008;
        private const int FUNCTION_WRITER_LIGHT_ERROR_COUNT = 1009;
        private const int FUNCTION_WRITER_WARNING_COUNT = 1010;
        private const int FUNCTION_WRITER_ADD_REPORTS = 1011;
        private const int FUNCTION_WRITER_CHECK_AND_THROW_ERRORS = 1012;
        private const int FUNCTION_WRITER_CHECK_AND_THROW_ERROR_WARNINGS = 1013;
        private const int FUNCTION_WRITER_CLEAR = 1014;
        private const int FUNCTION_WRITER_CLEAR_COUNTERS = 1015;
        private const int FUNCTION_WRITER_CLEAR_LAST_ERROR_REPORT = 1016;
        private const int FUNCTION_WRITER_CLOSE = 1017;


        internal ReportWriterWrapper(Client client, int objectId)
        {
            _client = client;
            _objectId = objectId;
        }

        public string Language { set => _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_LANGUAGE, BigEndianBitConverter.GetBytes(value), _objectId)); }

        public Report LastErrorReport
        {
            get
            {
                var res = _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_LAST_ERROR_REPORT, null, _objectId));
                return new Report(BigEndianBitConverter.ToInt32(res.Data, 0), _client);
            }
        }

        public int Size
        {
            get
            {
                var res = _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_SIZE, null, _objectId));
                return BigEndianBitConverter.ToInt32(res.Data, 0);
            }
        }

        public bool Fatals
        {
            get
            {
                var res = _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_FATALS, null, _objectId));
                return BigEndianBitConverter.ToBoolean(res.Data, 0);
            }
        }

        public bool Errors
        {
            get
            {
                var res = _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_ERRORS, null, _objectId));
                return BigEndianBitConverter.ToBoolean(res.Data, 0);
            }
        }

        public bool ErrorWarnings
        {
            get
            {
                var res = _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_ERRORWARNINGS, null, _objectId));
                return BigEndianBitConverter.ToBoolean(res.Data, 0);
            }
        }

        public int FatalErrorCount
        {
            get
            {
                var res = _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_FATAL_COUNT, null, _objectId));
                return BigEndianBitConverter.ToInt32(res.Data, 0);
            }
        }

        public int ErrorCount
        {
            get
            {
                var res = _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_ERROR_COUNT, null, _objectId));
                return BigEndianBitConverter.ToInt32(res.Data, 0);
            }
        }



        public int LightErrorCount
        {
            get
            {
                var res = _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_LIGHT_ERROR_COUNT, null, _objectId));
                return BigEndianBitConverter.ToInt32(res.Data, 0);
            }
        }

        public int WarningCount
        {
            get
            {
                var res = _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_WARNING_COUNT, null, _objectId));
                return BigEndianBitConverter.ToInt32(res.Data, 0);
            }
        }

        public ReportReader ReportReader => throw new NotImplementedException();

        public int ObjectId => _objectId;

        public void AddReports(ReportReader reporter)
        {
            if (!(reporter is RemoteObject ro))
            {
                throw new ArgumentException("Can only use with remote objects.");
            }
            _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_ADD_REPORTS, BigEndianBitConverter.GetBytes(ro.ObjectId), _objectId));
        }

        public void CheckAndThrowErrors()
        {
            var response = _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_CHECK_AND_THROW_ERRORS, null, _objectId));
            if (response.Data != null)
            {
                using (var reader = response.Reader)
                    throw new SRuntimeException(reader.ReadString());
            }
        }

        public void CheckAndThrowErrorWarnings()
        {
            var response = _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_CHECK_AND_THROW_ERROR_WARNINGS, null, _objectId));
            if (response.Data != null)
            {
                using (var reader = response.Reader)
                    throw new SRuntimeException(reader.ReadString());
            }
        }

        public void Clear()
        {
            _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_CLEAR, null, _objectId));
        }

        public void ClearCounters()
        {
            _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_CLEAR_COUNTERS, null, _objectId));
        }

        public void ClearLastErrorReport()
        {
            _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_CLEAR_LAST_ERROR_REPORT, null, _objectId));
        }

        public void Close()
        {
            _client.SendRequestWithResponse(new Request(FUNCTION_WRITER_CLOSE, null, _objectId));
        }
    }
}
