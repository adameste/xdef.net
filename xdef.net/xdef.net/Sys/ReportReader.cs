using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xdef.net.Sys
{
    public interface ReportReader
    {
		/// <summary>Get next report from the list or null.</summary>
		/// <returns>The report or null.</returns>
		Report Report { get; }

			/// <summary>Close the report stream.</summary>
			void Close();

			/// <summary>Write reports to output stream.</summary>
			/// <param name="out">The PrintStream where reports are printed.</param>
			void PrintReports(Stream output);

			/// <summary>Write reports to output stream.</summary>
			/// <param name="language">language id (ISO-639).</param>
			/// <param name="out">The PrintStream where reports are printed.</param>
			void PrintReports(Stream output, string language);

			/// <summary>Write reports to String (in actual language).</summary>
			/// <returns>the String with reports.</returns>
			string PrintToString();

			/// <summary>Write reports to String in specified language.</summary>
			/// <param name="language">language id (ISO-639).</param>
			/// <returns>the String with reports.</returns>
			string PrintToString(string language);

			/// <summary>Write reports from this reporter reader to report writer.</summary>
			/// <param name="reporter">OutputStreamWriter where to write,</param>
			void WriteReports(ReportWriter reporter);
		}
}
