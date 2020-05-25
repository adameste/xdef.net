using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using xdef.net.Connection;

namespace xdef.net.Sys
{
    public class Reporter : RemoteObject, ReportReader, ReportWriter
    {
        internal ReportReaderWrapper _reportReaderWrapper;
        internal ReportWriterWrapper _reportWriterWrapper;

        public Reporter() : base(0, XD.Instance.Client)
        {
            
        }

        public Report Report => ((ReportReader)_reportReaderWrapper).Report;

        public string Language { set => _reportWriterWrapper.Language = value; }

        public Report LastErrorReport => _reportWriterWrapper.LastErrorReport;

        public int Size => _reportWriterWrapper.Size;

        public bool Fatals => _reportWriterWrapper.Fatals;

        public bool Errors => _reportWriterWrapper.Errors;

        public bool ErrorWarnings => _reportWriterWrapper.ErrorWarnings;

        public int FatalErrorCount => _reportWriterWrapper.FatalErrorCount;

        public int ErrorCount => _reportWriterWrapper.ErrorCount;

        public int LightErrorCount => _reportWriterWrapper.LightErrorCount;

        public int WarningCount => _reportWriterWrapper.WarningCount;

        public ReportReader ReportReader => this;

        int ReportReader.ObjectId => ((ReportReader)_reportReaderWrapper).ObjectId;

        int ReportWriter.ObjectId => _reportWriterWrapper.ObjectId;

        public void AddReports(ReportReader reporter)
        {
            _reportWriterWrapper.AddReports(reporter);
        }

        public void CheckAndThrowErrors()
        {
            _reportWriterWrapper.CheckAndThrowErrors();
        }

        public void CheckAndThrowErrorWarnings()
        {
            _reportWriterWrapper.CheckAndThrowErrorWarnings();
        }

        public void Clear()
        {
            _reportWriterWrapper.Clear();
        }

        public void ClearCounters()
        {
            _reportWriterWrapper.ClearCounters();
        }

        public void ClearLastErrorReport()
        {
            _reportWriterWrapper.ClearLastErrorReport();
        }

        public void Close()
        {
            ((ReportReader)_reportReaderWrapper).Close();
        }

        public void PrintReports(Stream output)
        {
            ((ReportReader)_reportReaderWrapper).PrintReports(output);
        }

        public void PrintReports(Stream output, string language)
        {
            ((ReportReader)_reportReaderWrapper).PrintReports(output, language);
        }

        public string PrintToString()
        {
            return ((ReportReader)_reportReaderWrapper).PrintToString();
        }

        public string PrintToString(string language)
        {
            return ((ReportReader)_reportReaderWrapper).PrintToString(language);
        }

        public void WriteReports(ReportWriter reporter)
        {
            ((ReportReader)_reportReaderWrapper).WriteReports(reporter);
        }
    }
}
