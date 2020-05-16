using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net.Sys
{
    public interface ReportWriter
    {
		/// <summary>Language (ISO-639 or ISO-639-2).</summary>
		/// <remarks>
		/// Set language (ISO-639 or ISO-639-2). This method takes an effect only if
		/// the reporter output is printed as a text to the output stream.
		/// </remarks>
		/// <value name="language">language id (ISO-639).</value>
		string Language { set; }


		/// <summary>Get last error report.</summary>
		/// <value>
		/// last error report (or <tt>null</tt> if last report is not
		/// available).
		/// </value>
		Report LastErrorReport { get; }

		/// <summary>Clear last error report.</summary>
		/// <remarks>
		/// Clear last error report. If last report has been available it will be
		/// erased (i.e. result of <tt>getLastReport()</tt> will be null. However,
		/// the report has already been written to the report file.
		/// </remarks>
		void ClearLastErrorReport();

		/// <summary>Clear counters of fatal errors, errors and warnings.</summary>
		void ClearCounters();

		/// <summary>Clear the report file.</summary>
		/// <remarks>
		/// Clear the report file. All report items will be erased from the file.
		/// Also last error report is cleared.
		/// throws KException if it is not possible to clear reports.
		/// </remarks>
		void Clear();

		/// <summary>Get total number of reports.</summary>
		/// <remarks>The number of generated reports.</remarks>
		int Size { get; }

		/// <summary>Check if fatal errors were generated.</summary>
		/// <remarks>true is errors reports are present.</remarks>
		bool Fatals { get; }

		/// <summary>Check if errors and/or fatal errors were generated.</summary>
		/// <remarks>true is errors reports are present.</remarks>
		bool Errors { get;  }

		/// <summary>Check if warnings and/or errors and/or fatal errors were generated.</summary>
		/// <remarks>true is warnings or errors reports are present.</remarks>
		bool ErrorWarnings { get; }

		/// <summary>Get number of fatal items.</summary>
		/// <remarks>The number of generated fatal errors.</remarks>
		int FatalErrorCount { get; }

		/// <summary>Get number of error items.</summary>
		/// <remarks>The number of errors.</remarks>
		int ErrorCount { get; }

		/// <summary>Get number of light error items.</summary>
		/// <remarks>The number of light errors.</remarks>
		int LightErrorCount { get; }

		/// <summary>Get number of warning items.</summary>
		/// <remarks>The number of generated warnings.</remarks>
		int WarningCount { get; }

		/// <summary>
		/// Closes the reportWriter and creates report reader for reading created
		/// report data.
		/// </summary>
		/// <remarks>
		/// Closes the reportWriter and creates report reader for reading created
		/// report data. If reader can't be created the SRuntimeException is thrown.
		/// </remarks>
		/// <returns>report reader created from report writer.</returns>
		ReportReader ReportReader { get; }

		/// <summary>Close report writer.</summary>
		void Close();


		/// <summary>Check error reports stored in report writer.</summary>
		/// <remarks>
		/// Check error reports stored in report writer. Return normally if
		/// in no errors are found, otherwise throw exception with list of
		/// error messages (max. MAX_REPORTS messages).
		/// </remarks>
		/// <exception cref="SRuntimeException">if errors has been generated.</exception>
		/// <exception cref="org.xdef.sys.SRuntimeException"/>
		void CheckAndThrowErrors();

		/// <summary>Check if error and warning reports were stored in report writer.</summary>
		/// <remarks>
		/// Check if error and warning reports were stored in report writer. Return
		/// normally if in no errors or warnings are found, otherwise throw
		/// exception with the  list of error messages (max. MAX_REPORTS messages).
		/// </remarks>
		/// <exception cref="SRuntimeException">if errors or warnings has been generated.</exception>
		/// <exception cref="org.xdef.sys.SRuntimeException"/>
		void CheckAndThrowErrorWarnings();

		/// <summary>Add to this reporter reports from report reader.</summary>
		/// <param name="reporter">report reader with reports to be added.</param>
		void AddReports(ReportReader reporter);

		int ObjectId { get; }
	}
}
