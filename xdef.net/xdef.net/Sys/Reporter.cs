using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using xdef.net.Connection;

namespace xdef.net.Sys
{
    public class Reporter : RemoteObject, ReportReader, ReportWriter
    {
        protected ReportReaderWrapper _reportReaderWrapper;
        protected ReportWriterWrapper _reportWriterWrapper;

        public Reporter() : base(0, XD.Instance.Client)
        {
            
        }


        Report ReportReader.Report => _reportReaderWrapper.Report;

        string ReportWriter.Language { set => _reportWriterWrapper.Language = value; }

        Report ReportWriter.LastErrorReport => _reportWriterWrapper.LastErrorReport;

        int ReportWriter.Size => _reportWriterWrapper.Size;

        bool ReportWriter.Fatals => _reportWriterWrapper.Fatals;

        bool ReportWriter.Errors => _reportWriterWrapper.Errors;

        bool ReportWriter.ErrorWarnings => _reportWriterWrapper.ErrorWarnings;

        int ReportWriter.FatalErrorCount => _reportWriterWrapper.FatalErrorCount;

        int ReportWriter.ErrorCount => _reportWriterWrapper.ErrorCount;

        int ReportWriter.LightErrorCount => _reportWriterWrapper.LightErrorCount;

        int ReportWriter.WarningCount => _reportWriterWrapper.WarningCount;

        ReportReader ReportWriter.ReportReader => this;

        void ReportWriter.AddReports(ReportReader reporter)
        {
            _reportWriterWrapper.AddReports(reporter);
        }

        void ReportWriter.CheckAndThrowErrors()
        {
            _reportWriterWrapper.CheckAndThrowErrors();
        }

        void ReportWriter.CheckAndThrowErrorWarnings()
        {
            _reportWriterWrapper.CheckAndThrowErrorWarnings();
        }

        void ReportWriter.Clear()
        {
            _reportWriterWrapper.Clear();
        }

        void ReportWriter.ClearCounters()
        {
            _reportWriterWrapper.ClearCounters();
        }

        void ReportWriter.ClearLastErrorReport()
        {
            _reportWriterWrapper.ClearLastErrorReport();
        }

        void ReportReader.Close()
        {
            _reportReaderWrapper.Close();
        }

        void ReportWriter.Close()
        {
            _reportWriterWrapper.Close();
        }

        void ReportReader.PrintReports(Stream output)
        {
            _reportReaderWrapper.PrintReports(output);
        }

        void ReportReader.PrintReports(Stream output, string language)
        {
            _reportReaderWrapper.PrintReports(output, language);
        }

        string ReportReader.PrintToString()
        {
            return _reportReaderWrapper.PrintToString();
        }

        string ReportReader.PrintToString(string language)
        {
            return _reportReaderWrapper.PrintToString(language);
        }

        void ReportReader.WriteReports(ReportWriter reporter)
        {
            _reportReaderWrapper.WriteReports(reporter);
        }
    }
}
