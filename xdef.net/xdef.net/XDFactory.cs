﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using xdef.net.Connection;
using xdef.net.Connection.Library;
using xdef.net.Sys;
using xdef.net.Utils;

namespace xdef.net
{
    public sealed class XDFactory : RemoteObject
    {
        private const int FUNCTION_GETXDVERSION = 1;
        private const int FUNCTION_GETXDBUILDER_1 = 2;
        private const int FUNCTION_GETXDBUILDER_2 = 3;
        private const int FUNCTION_COMPILEXD_1 = 4;
        private const int FUNCTION_COMPILEXD_2 = 5;
        private const int FUNCTION_COMPILEXD_3 = 6;
        private const int FUNCTION_COMPILEXD_4 = 7;
        private const int FUNCTION_COMPILEXD_5 = 8;
        private const int FUNCTION_COMPILEXD_6 = 9;
        private const int FUNCTION_COMPILEXD_7 = 10;
        private const int FUNCTION_XPARSE_1 = 11;
        private const int FUNCTION_XPARSE_2 = 12;
        private const int FUNCTION_CREATEXDINPUT_1 = 13;
        private const int FUNCTION_CREATEXDINPUT_2 = 14;
        private const int FUNCTION_CREATEXDINPUT_3 = 15;
        private const int FUNCTION_CREATEXDOUTPUT_1 = 16;
        private const int FUNCTION_CREATEXDOUTPUT_2 = 17;
        private const int FUNCTION_CREATEXDOUTPUT_3 = 18;
        private const int FUNCTION_CREATEXDELEMENT = 19;
        private const int FUNCTION_CREATEXDNAMEDVALUE = 20;
        private const int FUNCTION_CREATEXDCONTAINER_1 = 21;
        private const int FUNCTION_CREATEXDCONTAINER_2 = 22;
        private const int FUNCTION_CREATESQLSERVICE_1 = 23;
        private const int FUNCTION_CREATESQLSERVICE_2 = 24;
        private const int FUNCTION_CREATEXDRESULTSET_1 = 25;
        private const int FUNCTION_CREATEXDRESULTSET_2 = 26;
        private const int FUNCTION_CREATEXDXMLOUTSTREAM_1 = 27;
        private const int FUNCTION_CREATEXDXMLOUTSTREAM_2 = 28;
        private const int FUNCTION_CREATEPARSERESULT = 29;
        private const int FUNCTION_CREATEXDVALUE = 30;
        private const int FUNCTION_WRITEXDPOOL_1 = 31;
        private const int FUNCTION_WRITEXDPOOL_2 = 32;
        private const int FUNCTION_WRITEXDPOOL_3 = 33;
        private const int FUNCTION_READXDPOOL_1 = 34;
        private const int FUNCTION_READXDPOOL_2 = 35;
        private const int FUNCTION_READXDPOOL_3 = 36;
        private const int FUNCTION_READXDPOOL_4 = 37;

        private const int OBJECT_TYPE_FILE = 1;
        private const int OBJECT_TYPE_STREAM = 2;
        private const int OBJECT_TYPE_STRING = 3;
        private const int OBJECT_TYPE_URL = 4;


        internal XDFactory(int objectId, Client client) : base(objectId, client)
        {
        }

        // Autogenerated method
        //  public static java.lang.String getXDVersion();
        /// <summary>Get version of this implementation of X-definition.</summary>
        /// <returns>version of this implementation of X-definition.</returns>
        public string GetXDVersion()
        {
            var res = SendRequestWithResponse(new Request(FUNCTION_GETXDVERSION, null, ObjectId));
            using (var reader = res.Reader)
            {
                return reader.ReadString();
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDBuilder getXDBuilder(java.util.Properties);
        /// <summary>Creates instance of XDBuilder with properties.</summary>
		/// <param name="props">
		/// Properties or null -
		/// see
		/// <see cref="XDConstants"/>
		/// .
		/// </param>
		/// <returns>created XDBuilder.</returns>
        public XDBuilder GetXDBuilder(Utils.Properties props)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(props);
                var res = SendRequestWithResponse(new Request(FUNCTION_GETXDBUILDER_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDBuilder(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDBuilder getXDBuilder(org.xdef.sys.ReportWriter, java.util.Properties);
        /// <summary>Creates instance of XDBuilder with properties.</summary>
		/// <param name="reporter">the ReportWriter to be used for error reporting.</param>
		/// <param name="props">
		/// Properties or <tt>null</tt> -
		/// see
		/// <see cref="XDConstants"/>
		/// .
		/// </param>
		/// <returns>created XDBuilder.</returns>
        public XDBuilder GetXDBuilder(ReportWriter reporter, Utils.Properties props)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(reporter.ObjectId);
                builder.Add(props);
                var res = SendRequestWithResponse(new Request(FUNCTION_GETXDBUILDER_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDBuilder(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDPool compileXD(java.util.Properties, java.lang.String[]) throws org.xdef.sys.SRuntimeException;
        /// <summary>Compile XDPool from sources.</summary>
		/// <param name="props">Properties or <tt>null</tt>.</param>
		/// <param name="parameters">list of strings with X-definition file names.</param>
		/// <returns>generated XDPool.</returns>
		/// <exception cref="RemoteCallException">if an error occurs.</exception>
        public XDPool CompileXD(Utils.Properties props, params string[] parameters)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(props);
                builder.Add(parameters.Count());
                foreach (var it in parameters) builder.Add(it);
                var res = SendRequestWithResponse(new Request(FUNCTION_COMPILEXD_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDPool(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDPool compileXD(java.util.Properties, java.net.URL[]) throws org.xdef.sys.SRuntimeException;
        /// <summary>Compile XDPool from URLs.</summary>
		/// <param name="props">Properties or <tt>null</tt>.</param>
		/// <param name="parameters">list of URLs with X-definition sources.</param>
		/// <returns>generated XDPool.</returns>
		/// <exception cref="RemoteCallException">if an error occurs.</exception>
        public XDPool CompileXD(Utils.Properties props, params Uri[] parameters)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(props);
                builder.Add(parameters.Count());
                foreach (var it in parameters) builder.Add(it.AbsoluteUri);
                var res = SendRequestWithResponse(new Request(FUNCTION_COMPILEXD_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDPool(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDPool compileXD(java.util.Properties, java.io.File[]) throws org.xdef.sys.SRuntimeException;
        /// <summary>Compile XDPool from InputStreams.</summary>
		/// <param name="props">Properties or <tt>null</tt>.</param>
		/// <param name="parameters">list of files with X-definition sources.</param>
		/// <returns>generated XDPool.</returns>
		/// <exception cref="org.xdef.sys.SRuntimeException">if an error occurs.</exception>
        public XDPool CompileXD(Utils.Properties props, params FilePath[] parameters)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(props);
                builder.Add(parameters.Count());
                foreach (var it in parameters) builder.Add(it.JavaPath);
                var res = SendRequestWithResponse(new Request(FUNCTION_COMPILEXD_3, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDPool(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDPool compileXD(java.util.Properties, java.io.InputStream[]) throws org.xdef.sys.SRuntimeException;
        /// <summary>Compile XDPool from InputStreams.</summary>
		/// <param name="props">Properties or <tt>null</tt>.</param>
		/// <param name="parameters">list of files with X-definition sources.</param>
		/// <returns>generated XDPool.</returns>
		/// <exception cref="RemoteCallException">if an error occurs.</exception>
        public XDPool CompileXD(Utils.Properties props, params Stream[] parameters)
        {
            if (parameters.Any(p => !p.CanRead)) throw new ArgumentException("Input stream is not readable");
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(props);
                builder.Add(parameters.Count());
                foreach (var it in parameters)
                {
                    var wrap = new RemoteStreamWrapper(_client, it);
                    builder.Add(_client.RegisterLocalObject(wrap));
                }
                var res = SendRequestWithResponse(new Request(FUNCTION_COMPILEXD_4, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDPool(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDPool compileXD(java.util.Properties, java.lang.Object[], java.lang.String[]) throws org.xdef.sys.SRuntimeException;
        /// <summary>Compile XDPool from sources and assign the sourceId to each source.</summary>
		/// <param name="props">Properties or <tt>null</tt>.</param>
		/// <param name="sources">
		/// array with source data with X-definitions source data.
		/// (The type of items can only be either the InputStreams or the String
		/// containing an XML document).
		/// </param>
		/// <param name="sourceIds">
		/// array with sourceIds (corresponding to the items
		/// in the argument sources).
		/// </param>
		/// <returns>generated XDPool.</returns>
		/// <exception cref="RemoteCallException">if an error occurs.</exception>
        public XDPool CompileXD(Utils.Properties props, IEnumerable<object> sources, IEnumerable<string> sourceIds)
        {
            if (sources.Count() != sourceIds.Count()) throw new ArgumentException("Object and sourceNames count mismatch.");
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(props);
                SerializeObjectSources(builder, sources);
                builder.Add(sourceIds.Count());
                foreach (var it in sourceIds) builder.Add(it);
                var res = SendRequestWithResponse(new Request(FUNCTION_COMPILEXD_5, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDPool(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDPool compileXD(java.util.Properties, java.lang.Object...) throws org.xdef.sys.SRuntimeException;
        /// <summary>Compile XDPool from source.</summary>
		/// <param name="props">Properties or <tt>null</tt>.</param>
		/// <param name="parameters">list of sources, source pairs or external classes.</param>
		/// <returns>generated XDPool.</returns>
		/// <exception cref="RemoteCallException">if an error occurs.</exception>
        public XDPool CompileXD(Utils.Properties props, params object[] parameters)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(props);
                SerializeObjectSources(builder, parameters);
                var res = SendRequestWithResponse(new Request(FUNCTION_COMPILEXD_6, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDPool(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDPool compileXD(org.xdef.sys.ReportWriter, java.util.Properties, java.lang.Object...) throws org.xdef.sys.SRuntimeException;
        /// <summary>Compile XDPool from source.</summary>
		/// <param name="reporter">the ReportWriter to be used for error reporting. Only array reporter supported due to bug in original library.</param>
		/// <param name="props">Properties or <tt>null</tt>.</param>
		/// <param name="parameters">list of sources, source pairs or external classes.</param>
		/// <returns>generated XDPool.</returns>
		/// <exception cref="RemoteCallException">if an error occurs.</exception>
        public XDPool CompileXD(ArrayReporter reporter, Utils.Properties props, params object[] parameters)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(reporter.ObjectId);
                builder.Add(props);
                SerializeObjectSources(builder, parameters);
                var res = SendRequestWithResponse(new Request(FUNCTION_COMPILEXD_7, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDPool(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDDocument xparse(java.io.InputStream, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Parse XML with X-definition declared in source input stream.</summary>
		/// <param name="source">where to read XML.</param>
		/// <param name="reporter">used for error messages or <tt>null</tt>.</param>
		/// <returns>created XDDocument object.</returns>
		/// <exception cref="RemoteCallException">if an error occurs.</exception>
        public XDDocument Xparse(Stream source, ReportWriter reporter)
        {
            if (!source.CanRead) throw new ArgumentException();
            using (var builder = new BigEndianDataBuilder())
            {
                var wrap = new RemoteStreamWrapper(_client, source);
                builder.Add(_client.RegisterLocalObject(wrap));
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XPARSE_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDDocument(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDDocument xparse(java.lang.String, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Parse XML with X-definition declared in source.</summary>
		/// <param name="source">URL, pathname direct to XML or direct XML.</param>
		/// <param name="reporter">used for error messages or <tt>null</tt>.</param>
		/// <returns>created XDDocument object.</returns>
		/// <exception cref="org.xdef.sys.SRuntimeException">if an error occurs.</exception>
        public XDDocument Xparse(string source, ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(source);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XPARSE_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDDocument(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDInput createXDInput(java.io.InputStream, boolean);
        /// <summary>Creates XDInput from InputStream.</summary>
		/// <param name="value">the stream.</param>
		/// <param name="xmlFormat">
		/// if <tt>true</tt> the input data are in XML format,
		/// otherwise in string format.
		/// </param>
		/// <returns>the XDInput object.</returns>
        public XDInput CreateXDInput(Stream value, bool xmlFormat)
        {
            if (!value.CanRead) throw new ArgumentException("Stream not readable");
            using (var builder = new BigEndianDataBuilder())
            {
                var wrap = new RemoteStreamWrapper(_client, value);
                builder.Add(_client.RegisterLocalObject(wrap));
                builder.Add(xmlFormat);
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDINPUT_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDInput(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDInput createXDInput(java.io.InputStreamReader, boolean);
        /// <summary>Creates XDInput from InputStream.</summary>
		/// <param name="value">the stream.</param>
		/// <param name="xmlFormat">
		/// if <tt>true</tt> the input data are in XML format,
		/// otherwise in string format.
		/// </param>
		/// <returns>the XDInput object.</returns>
        public XDInput CreateXDInput(StreamReader value, bool xmlFormat)
        {
            return CreateXDInput(value.BaseStream, xmlFormat);
        }


        // Autogenerated method
        //  public static org.xdef.XDInput createXDInput(org.xdef.sys.ReportReader);
        /// <summary>Creates XDInput from InputStream.</summary>
		/// <param name="value">ReportReader.</param>
		/// <returns>the XDInput object.</returns>
        public XDInput CreateXDInput(ReportReader value)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(value.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDINPUT_3, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDInput(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDOutput createXDOutput(org.xdef.sys.ReportWriter);
        /// <summary>Creates XDOutput from reporter.</summary>
		/// <param name="value">the reporter.</param>
		/// <returns>the XDOutput object.</returns>
        public XDOutput CreateXDOutput(ReportWriter value)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(value.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDOUTPUT_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDOutput(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public static org.xdef.XDOutput createXDOutput(java.io.Writer, boolean);
        /// <summary>Creates XDOutput from Stream.</summary>
		/// <param name="value">Writer object.</param>
		/// <param name="xmlFormat">
		/// if <tt>true</tt> the output will be in XML format,
		/// otherwise in string format.
		/// </param>
		/// <returns>the XDOutput object.</returns>
        public XDOutput CreateXDOutput(Stream value, bool xmlFormat)
        {
            if (!value.CanWrite) throw new ArgumentException("Stream not writable.");
            using (var builder = new BigEndianDataBuilder())
            {
                var wrap = new RemoteStreamWrapper(_client, value);
                builder.Add(_client.RegisterLocalObject(wrap));
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDOUTPUT_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDOutput(reader.ReadInt32(), _client);
                }
            }
        }

        /*
        // Autogenerated method
        //  public static org.xdef.XDOutput createXDOutput(java.io.PrintStream);
        public XDOutput CreateXDOutput(PrintStream arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDOUTPUT_3, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    throw new NotImplementedException();
                }
            }
        }
        */

        // Autogenerated method
        //  public static org.xdef.XDElement createXDElement(org.w3c.dom.Element);
        /// <summary>Creates XDElement from System.XML.Linq.XElement</summary>
		/// <param name="el">System.XML.Linq.XElement</param>
		/// <returns>XDElement object.</returns>
        public XDElement CreateXDElement(XElement el)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(el.ToString(SaveOptions.DisableFormatting));
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDELEMENT, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDElement(reader.ReadInt32(), _client);
                }
            }
        }

        /*
        // Autogenerated method
        //  public static org.xdef.XDNamedValue createXDNamedValue(java.lang.String, java.lang.Object);
        public XDNamedValue CreateXDNamedValue(string name, object value)
        {
            return new XDNamedValue(name, value);
        }


        // Autogenerated method
        //  public static org.xdef.XDContainer createXDContainer();
        public XDContainer CreateXDContainer()
        {
            var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDCONTAINER_1, null, ObjectId));
            using (var reader = res.Reader)
            {
                return new XDContainer(reader.ReadInt32(), _client);
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDContainer createXDContainer(java.lang.Object);
        public XDContainer CreateXDContainer(Object arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDCONTAINER_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    throw new NotImplementedException();
                }
            }
        }
        */

        // Autogenerated method
        //  public static org.xdef.XDService createSQLService(java.lang.String, java.lang.String, java.lang.String) throws org.xdef.sys.SRuntimeException;
        /// <summary>Creates XDService object with JDBC support.</summary>
		/// <param name="url">string with connection URL.</param>
		/// <param name="user">user name.</param>
		/// <param name="passw">password.</param>
		/// <returns>XDService object.</returns>
		/// <exception cref="RemoteCallException">if an error occurs.</exception>
        public XDService CreateSQLService(string url, string user, string passw)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(url).Add(user).Add(passw);
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATESQLSERVICE_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDService(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDService createSQLService(java.sql.Connection) throws org.xdef.sys.SRuntimeException;
        /* TODO
        public XDService CreateSQLService(Connection arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATESQLSERVICE_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    throw new NotImplementedException();
                }
            }
        }*/


        // Autogenerated method
        //  public static org.xdef.XDResultSet createXDResultSet(java.sql.ResultSet);
        /* TODO
        public XDResultSet CreateXDResultSet(ResultSet arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDRESULTSET_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    throw new NotImplementedException();
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDResultSet createXDResultSet(java.lang.String, java.sql.ResultSet);
        public XDResultSet CreateXDResultSet(String arg0, ResultSet arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDRESULTSET_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    throw new NotImplementedException();
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDXmlOutStream createXDXmlOutStream(java.io.Writer, java.lang.String, boolean);
        public XDXmlOutStream CreateXDXmlOutStream(Writer arg0, String arg1, boolean arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDXMLOUTSTREAM_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    throw new NotImplementedException();
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDXmlOutStream createXDXmlOutStream(java.lang.String, java.lang.String, boolean) throws java.io.IOException;
        public XDXmlOutStream CreateXDXmlOutStream(String arg0, String arg1, boolean arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDXMLOUTSTREAM_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    throw new NotImplementedException();
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDParseResult createParseResult(java.lang.String);
        public XDParseResult CreateParseResult(String arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEPARSERESULT, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    throw new NotImplementedException();
                }
            }
        }


        // Autogenerated method
        //  public static org.xdef.XDValue createXDValue(java.lang.Object);
        public XDValue CreateXDValue(Object arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_CREATEXDVALUE, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    throw new NotImplementedException();
                }
            }
        }*/


        // Autogenerated method
        //  public static final void writeXDPool(java.io.OutputStream, org.xdef.XDPool) throws java.io.IOException;
        /// <summary>
        /// Writes XDPool to provided stream.
        /// </summary>
        /// <param name="stream">Target stream.</param>
        /// <param name="pool">Pool to write</param>
        public void WriteXDPool(Stream stream, XDPool pool)
        {
            if (!stream.CanRead) throw new ArgumentException("Cannot read from stream.");
            using (var builder = new BigEndianDataBuilder())
            {
                var wrap = new RemoteStreamWrapper(_client, stream);
                builder.Add(_client.RegisterLocalObject(wrap));
                builder.Add(pool.ObjectId);
                SendRequestWithResponse(new Request(FUNCTION_WRITEXDPOOL_1, builder.Build(), ObjectId));
            }
        }


        // Autogenerated method
        //  public static final void writeXDPool(java.io.File, org.xdef.XDPool) throws java.io.IOException;
        /// <summary>
        /// Writes XDPool to given file.
        /// </summary>
        /// <param name="path">Filepath to write pool.</param>
        /// <param name="pool">Pool to write.</param>
        public void WriteXDPool(FilePath path, XDPool pool)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(path).Add(pool.ObjectId);
                SendRequestWithResponse(new Request(FUNCTION_WRITEXDPOOL_2, builder.Build(), ObjectId));
            }
        }


        // Autogenerated method
        //  public static final void writeXDPool(java.lang.String, org.xdef.XDPool) throws java.io.IOException;
        /// <summary>
        /// Writes XDPool to specified path.
        /// </summary>
        /// <param name="path">String path to file.</param>
        /// <param name="pool">Pool to write</param>
        public void WriteXDPool(string path, XDPool pool)
        {
            WriteXDPool(new FilePath(path), pool);
        }


        // Autogenerated method
        //  public static final org.xdef.XDPool readXDPool(java.io.InputStream) throws java.io.IOException;
        /// <summary>
        /// Reads XDPool from stream.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        /// <returns>XDPool object.</returns>
        public XDPool ReadXDPool(Stream stream)
        {
            if (!stream.CanRead) throw new ArgumentException("Stream is not readable.");
            using (var builder = new BigEndianDataBuilder())
            {
                var wrap = new RemoteStreamWrapper(_client, stream);
                builder.Add(_client.RegisterLocalObject(wrap));
                var res = SendRequestWithResponse(new Request(FUNCTION_READXDPOOL_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDPool(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static final org.xdef.XDPool readXDPool(java.io.File) throws java.io.IOException;
        /// <summary>
        /// Reads XDPool from specified file.
        /// </summary>
        /// <param name="file">Path to file with XDPool.</param>
        /// <returns>XDPool object.</returns>
        public XDPool ReadXDPool(FilePath file)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(file);
                var res = SendRequestWithResponse(new Request(FUNCTION_READXDPOOL_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDPool(reader.ReadInt32(), _client);
                }
            }
        }


        // Autogenerated method
        //  public static final org.xdef.XDPool readXDPool(java.lang.String) throws java.io.IOException;
        /// <summary>
        /// Reads XDPool from specified file.
        /// </summary>
        /// <param name="filePath">String path to file with XDPool.</param>
        /// <returns>XDPool object.</returns>
        public XDPool ReadXDPool(string filePath)
        {
            return ReadXDPool(new FilePath(filePath));
        }


        // Autogenerated method
        //  public static final org.xdef.XDPool readXDPool(java.net.URL) throws java.io.IOException;
        /// <summary>
        /// Reads XDPool from specified URL
        /// </summary>
        /// <param name="url">Url from which to read XDPool.</param>
        /// <returns>XDPool object.</returns>
        public XDPool ReadXDPool(Uri url)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                builder.Add(url);
                var res = SendRequestWithResponse(new Request(FUNCTION_READXDPOOL_4, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return new XDPool(reader.ReadInt32(), _client);
                }
            }
        }



        private void SerializeObjectSources(BigEndianDataBuilder builder, IEnumerable<object> sources)
        {
            var registeredObjects = new List<RemoteHandlingObject>();
            builder.Add(sources.Count());
            foreach (var it in sources)
            {
                if (it is string s)
                {
                    builder.Add(OBJECT_TYPE_STRING);
                    builder.Add(s);
                }
                else if (it is Uri u)
                {
                    builder.Add(OBJECT_TYPE_URL);
                    builder.Add(u.AbsoluteUri);
                }
                else if (it is FilePath fp)
                {
                    builder.Add(OBJECT_TYPE_FILE);
                    builder.Add(fp.JavaPath);
                }
                else if (it is Stream stream)
                {
                    builder.Add(OBJECT_TYPE_STREAM);
                    var wrap = new RemoteStreamWrapper(_client, stream);
                    builder.Add(_client.RegisterLocalObject(wrap));
                    registeredObjects.Add(wrap);
                }
                else
                {
                    foreach (var jt in registeredObjects)
                        _client.DeleteLocalObject(jt.ObjectId);
                    if (it == null)
                        throw new ArgumentNullException();
                    else
                        throw new ArgumentException($"Invalid argument of type: {it.GetType().FullName}");
                }
            }
        }
    }
}
