using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using xdef.net.Connection;
using xdef.net.Connection.Library;
using xdef.net.Proc;
using xdef.net.Sys;
using xdef.net.Utils;

namespace xdef.net
{
    /// <summary>Provides processing of given X-definition.</summary>
    /// <remarks>
    /// Provides processing of given X-definition. For processing of X-definition you
    /// must create and instance of XDDocument created from XDPool with given
    /// X-definition in which is defined root (starting point) for next processing.
    /// Before starting of process you can set parameters of processing (variables,
    /// properties, standard input and output streams, user objects). The document
    /// can be used for validation and processing of the input XML data ("xparse"
    /// methods) or for construction of a XML object (methods "xcreate"). Note that
    /// XDDocument is the root node of processing and it is the extension of
    /// the interface
    /// <see cref="XXNode"/>
    /// . You can read of or set to
    /// variables of a XDPool by methods of
    /// <see cref="XXNode"/>
    /// :
    /// <p>
    /// <see cref="XXNode.getVariable(string)"/>
    /// ,
    /// <see cref="XXNode.setVariable(string, object)"/>
    /// ,
    /// <see cref="XXNode.setUserObject(object)"/>
    /// ,
    /// <see cref="XXNode.getUserObject()"/>
    /// ,
    /// <see cref="XXNode.getUserObject()"/>
    /// ,
    /// <see cref="XXNode.setXDContext(org.w3c.dom.Node)"/>
    /// .
    /// <see cref="XXNode.getXDContext()"/>
    /// .</p>
    /// <p>Typical example of validation:</p>
    /// <pre><tt>
    /// //get instance of XDDocument with X-definition given by name
    /// XDDocument xDoc = xp.createXDDocument(name);
    /// ArrayReporter reporter = new ArrayReporter(); // here will be written errors
    /// ... set variables if necessary - see
    /// <see cref="XXNode"/>
    /// Element el = xd.xparse(sourceXml, reporter); //validate and process data
    /// //now we have root element of parsed source data errors in variable el
    /// //and list of errors in reporter
    /// //test if an error was reported
    /// if (reporter.errorWarnings()) {//error or warning reported?
    /// reporter.getReportReader().printReports(System.err);
    /// } else {//no errors
    /// ... get variables if necessary - see
    /// <see cref="XXNode"/>
    /// ....
    /// }</tt></pre>
    /// <p>Typical example of construction:</p>
    /// <pre><tt>
    /// //get instance of XDDocument with X-definition given by name
    /// XDDocument xDoc = xp.createXDDocument(name);
    /// ArrayReporter reporter = new ArrayReporter(); // here will be written errors
    /// ... set data source see
    /// <see cref="XXNode"/>
    /// //construct required element.
    /// Element el = xd.xcreate(nsuri, //namespace of required model or null
    /// name, // name of required model (in given X-definition)
    /// reporter);
    /// </tt></pre>
    /// </remarks>
    /// <author>Štěpán Adámek</author>
    public sealed class XDDocument : RemoteObject
    {
        private const int FUNCTION_SETPROPERTIES = 1;
        private const int FUNCTION_SETPROPERTY = 2;
        private const int FUNCTION_GETPROPERTIES = 3;
        private const int FUNCTION_ISCREATEMODE = 4;
        private const int FUNCTION_GETDOCUMENT = 5;
        private const int FUNCTION_SETROOTMODEL = 6;
        private const int FUNCTION_XPARSE_1 = 7;
        private const int FUNCTION_XPARSE_2 = 8;
        private const int FUNCTION_XPARSE_3 = 9;
        private const int FUNCTION_XPARSE_4 = 10;
        private const int FUNCTION_XPARSE_5 = 11;
        private const int FUNCTION_XPARSE_6 = 12;
        private const int FUNCTION_XPARSE_7 = 13;
        private const int FUNCTION_XCREATE_1 = 14;
        private const int FUNCTION_XCREATE_2 = 15;
        private const int FUNCTION_XCREATE_3 = 16;
        private const int FUNCTION_PARSEXCOMPONENT_1 = 17;
        private const int FUNCTION_PARSEXCOMPONENT_2 = 18;
        private const int FUNCTION_PARSEXCOMPONENT_3 = 19;
        private const int FUNCTION_PARSEXCOMPONENT_4 = 20;
        private const int FUNCTION_PARSEXCOMPONENT_5 = 21;
        private const int FUNCTION_PARSEXCOMPONENT_6 = 22;
        private const int FUNCTION_JPARSE_1 = 23;
        private const int FUNCTION_JPARSE_2 = 24;
        private const int FUNCTION_JPARSE_3 = 25;
        private const int FUNCTION_JPARSE_4 = 26;
        private const int FUNCTION_JPARSE_5 = 27;
        private const int FUNCTION_JPARSE_6 = 28;
        private const int FUNCTION_JPARSEXCOMPONENT_1 = 29;
        private const int FUNCTION_JPARSEXCOMPONENT_2 = 30;
        private const int FUNCTION_JPARSEXCOMPONENT_3 = 31;
        private const int FUNCTION_JPARSEXCOMPONENT_4 = 32;
        private const int FUNCTION_JPARSEXCOMPONENT_5 = 33;
        private const int FUNCTION_JPARSEXCOMPONENT_6 = 34;
        private const int FUNCTION_GETSTDOUT = 35;
        private const int FUNCTION_GETSTDERR = 36;
        private const int FUNCTION_GETSTDIN = 37;
        private const int FUNCTION_SETSTREAMWRITER_1 = 38;
        private const int FUNCTION_SETSTREAMWRITER_2 = 39;
        private const int FUNCTION_SETSTREAMWRITER_3 = 40;
        private const int FUNCTION_SETSTDOUT_1 = 41;
        private const int FUNCTION_SETSTDOUT_2 = 42;
        private const int FUNCTION_SETSTDIN_1 = 43;
        private const int FUNCTION_SETSTDOUT_3 = 44;
        private const int FUNCTION_SETSTDIN_2 = 45;
        private const int FUNCTION_SETDEBUGGER = 46;
        private const int FUNCTION_GETDEBUGGER = 47;
        private const int FUNCTION_SETDEBUG = 48;
        private const int FUNCTION_ISDEBUG = 49;
        private const int FUNCTION_PREPAREROOTXXELEMENTNS = 50;
        private const int FUNCTION_PREPAREROOTXXELEMENT = 51;
        private const int FUNCTION_GETIMPLPROPERTIES = 52;
        private const int FUNCTION_GETIMPLPROPERTY = 53;
        private const int FUNCTION_ISLEGALDATE = 54;
        private const int FUNCTION_GETMINYEAR = 55;
        private const int FUNCTION_SETMINYEAR = 56;
        private const int FUNCTION_GETMAXYEAR = 57;
        private const int FUNCTION_SETMAXYEAR = 58;
        private const int FUNCTION_GETSPECIALDATES = 59;
        private const int FUNCTION_SETSPECIALDATES = 60;
        private const int FUNCTION_CHECKDATELEGAL = 61;
        private const int FUNCTION_PRINTREPORTS = 62;
        private const int FUNCTION_GETLEXICONLANGUAGE = 63;
        private const int FUNCTION_SETLEXICONLANGUAGE = 64;
        private const int FUNCTION_XTRANSLATE_1 = 65;
        private const int FUNCTION_XTRANSLATE_2 = 66;


       
        internal XDDocument(int objectId, Client client) : base(objectId, client)
        {
        }

        // Autogenerated method
        //  public abstract void setProperties(java.util.Properties);
        /// <summary>Set properties.</summary>
		/// <param name="props">Properties.</param>
        public void SetProperties(Utils.Properties props)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(props);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETPROPERTIES, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract void setProperty(java.lang.String, java.lang.String);
        /// <summary>Set property.</summary>
		/// <remarks>
		/// Set property. If properties are null the new Properties object
		/// will be created.
		/// </remarks>
		/// <param name="key">name of property.</param>
		/// <param name="value">
		/// value of property or null. If the value is null the property
		/// is removed from properties.
		/// </param>
        public void SetProperty(string key, string value)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(key);
                builder.Add(value);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETPROPERTY, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract java.util.Properties getProperties();
        /// <summary>Get properties.</summary>
		/// <returns>assigned Properties.</returns>
        public Utils.Properties GetProperties()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETPROPERTIES, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return Utils.Properties.Deserialize(reader);
                }
            }
        }

        // Autogenerated method
        //  public abstract boolean isCreateMode();
        /// <summary>Check if create mode is running.</summary>
		/// <returns>true if and only if create mode is running.</returns>
        public bool IsCreateMode()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_ISCREATEMODE, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadBoolean();
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Document getDocument();
        /// <summary>Get document.</summary>
		/// <returns>The Document object (may be null).</returns>
        public XDocument GetDocument()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETDOCUMENT, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return XDocument.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract void setRootModel(org.xdef.model.XMElement);
        /*
        public void SetRootModel(XMElement arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETROOTMODEL, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }*/

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xparse(java.lang.String, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Parse and process XML source and return org.w3c.dom.Element.</summary>
		/// <param name="xmlData">string with pathname of XML file or XML source data.</param>
		/// <param name="reporter">
		/// report writer or <tt>null</tt>. If this argument is
		/// <tt>null</tt> and error reports occurs then SRuntimeException is thrown.
		/// </param>
		/// <returns>root element of parsed data.</returns>
		/// <exception cref="RemoteCallException">
		/// if reporter is <tt>null</tt> and an error
		/// was reported.
		/// </exception>
        public XElement Xparse(string xmlData, ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(xmlData);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XPARSE_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xparse(java.lang.String, java.lang.String, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Parse and process XML source and return org.w3c.dom.Element.</summary>
		/// <param name="xmlData">string with pathname of XML file or XML source data.</param>
		/// <param name="sourceId">name of source or null.</param>
		/// <param name="reporter">
		/// report writer or <tt>null</tt>. If this argument is
		/// null and error reports occurs then SRuntimeException is thrown.
		/// </param>
		/// <returns>root element of parsed data.</returns>
		/// <exception cref="RemoteCallException">
		/// if reporter is <tt>null</tt> and an error
		/// was reported.
		/// </exception>
        public XElement Xparse(string xmlData, string sourceId, ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(xmlData);
                builder.Add(sourceId);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XPARSE_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xparse(java.net.URL, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Parse and process XML source and return org.w3c.dom.Element.</summary>
		/// <param name="xmlData">URL pointing to XML source data.</param>
		/// <param name="reporter">
		/// report writer or <tt>null</tt>. If this argument is
		/// <tt>null</tt> and error reports occurs then RemoteCallException is thrown.
		/// </param>
		/// <returns>root element of parsed data.</returns>
		/// <exception cref="RemoteCallException">
		/// if reporter is <tt>null</tt> and an error
		/// was reported.
		/// </exception>
        public XElement Xparse(Uri xmlData, ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(xmlData);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XPARSE_3, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xparse(java.io.File, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Parse and process XML source and return org.w3c.dom.Element.</summary>
		/// <param name="xmlData">file with XML source data.</param>
		/// <param name="reporter">
		/// report writer or <tt>null</tt>. If this argument is
		/// <tt>null</tt> and error reports occurs then RemoteCallException is thrown.
		/// </param>
		/// <returns>root element of parsed data.</returns>
		/// <exception cref="RemoteCallException">
		/// if reporter is <tt>null</tt> and an error
		/// was reported.
		/// </exception>
        public XElement Xparse(FilePath xmlData, ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(xmlData);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XPARSE_4, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xparse(org.w3c.dom.Node, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Parse and process XML source and return org.w3c.dom.Element.</summary>
		/// <param name="xmlData">XML <tt>org.w3c.dom.Node</tt>.</param>
		/// <param name="reporter">
		/// report writer or <tt>null</tt>. If this argument is
		/// <tt>null</tt> and error reports occurs then SRuntimeException is thrown.
		/// </param>
		/// <returns>root element of parsed data.</returns>
		/// <exception cref="RemoteCallException">
		/// if reporter is <tt>null</tt> and an error
		/// was reported.
		/// </exception>
        public XElement Xparse(XNode xmlData, ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(xmlData);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XPARSE_5, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xparse(java.io.InputStream, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Parse and process XML source and return org.w3c.dom.Element.</summary>
		/// <param name="xmlData">input stream with XML source data.</param>
		/// <param name="reporter">
		/// report writer or <tt>null</tt>. If this argument is
		/// <tt>null</tt> and error reports occurs then SRuntimeException is thrown.
		/// </param>
		/// <returns>root element of parsed data.</returns>
		/// <exception cref="RemoteCallException">
		/// if reporter is <tt>null</tt> and an error
		/// was reported.
		/// </exception>
        public XElement Xparse(Stream xmlData, ReportWriter reporter)
        {
            if (!xmlData.CanRead) throw new ArgumentException("Stream no readable");
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(_client.RegisterLocalObject(new RemoteStreamWrapper(_client, xmlData)));
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XPARSE_6, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xparse(java.io.InputStream, java.lang.String, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Parse and process XML source and return org.w3c.dom.Element.</summary>
		/// <param name="xmlData">input stream with XML source data.</param>
		/// <param name="sourceId">name of source or <tt>null</tt>.</param>
		/// <param name="reporter">
		/// report writer or <tt>null</tt>. If this argument is
		/// <tt>null</tt> and error reports occurs then SRuntimeException is thrown.
		/// </param>
		/// <returns>root element of parsed data.</returns>
		/// <exception cref="RemoteCallException">
		/// if reporter is <tt>null</tt> and an error
		/// was reported.
		/// </exception>
        public XElement Xparse(Stream xmlData, string sourceId, ReportWriter reporter)
        {
            if (!xmlData.CanRead) throw new ArgumentException("Stream no readable");
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(_client.RegisterLocalObject(new RemoteStreamWrapper(_client, xmlData)));
                builder.Add(sourceId);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XPARSE_7, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xcreate(java.lang.String, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Run create mode - create element according to the X-definition model.</summary>
        /// <param name="name">the name of model of required element.</param>
        /// <param name="reporter">
        /// report writer or <tt>null</tt>. If this argument is
        /// <tt>null</tt> and error reports occurs then SRuntimeException is thrown.
        /// </param>
        /// <returns>root element of created XML document.</returns>
        /// <exception cref="RemoteCallException">
        /// if reporter is <tt>null</tt> and an error
        /// was reported.
        /// </exception>
        public XElement Xcreate(string name, ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(name);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XCREATE_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xcreate(java.lang.String, java.lang.String, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Run create mode - create element according to the X-definition model.</summary>
		/// <remarks>
		/// Run create mode - create element according to the X-definition model.
		/// If the parameter nsUri is not <tt>null</tt> then its assigned the model
		/// with given namespaceURI; in this case the parameter name may be
		/// qualified with a prefix.
		/// </remarks>
		/// <param name="nsUri">the namespace URI of result element (may be <tt>null</tt>).</param>
		/// <param name="name">the name of model of required element (may contain prefix).</param>
		/// <param name="reporter">
		/// report writer or <tt>null</tt>. If this argument is
		/// <tt>null</tt> and error reports occurs then SRuntimeException is thrown.
		/// </param>
		/// <returns>root element of created XML document.</returns>
		/// <exception cref="RemoteCallException">
		/// if reporter is <tt>null</tt> and an error
		/// was reported.
		/// </exception>
        public XElement Xcreate(string nsUri, string name, ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(nsUri);
                builder.Add(name);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XCREATE_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xcreate(javax.xml.namespace.QName, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>Run create mode - create element according to the X-definition model.</summary>
		/// <param name="qname">the QName of model of required element.</param>
		/// <param name="reporter">
		/// report writer or <tt>null</tt>. If this argument is
		/// <tt>null</tt> and error reports occurs then SRuntimeException is thrown.
		/// </param>
		/// <returns>root element of created XML document.</returns>
		/// <exception cref="RemoteCallException">
		/// if reporter is <tt>null</tt> and an error
		/// was reported.
		/// </exception>
        public XElement Xcreate(XmlQualifiedName qname, ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(qname);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XCREATE_3, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent parseXComponent(java.lang.String, java.lang.Class<?>, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /*
        public XComponent ParseXComponent(String arg0, Class<?> arg1, ReportWriter arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_PARSEXCOMPONENT_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent parseXComponent(java.io.File, java.lang.Class<?>, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public XComponent ParseXComponent(File arg0, Class<?> arg1, ReportWriter arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_PARSEXCOMPONENT_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent parseXComponent(java.net.URL, java.lang.Class<?>, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public XComponent ParseXComponent(URL arg0, Class<?> arg1, ReportWriter arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_PARSEXCOMPONENT_3, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent parseXComponent(java.io.InputStream, java.lang.Class<?>, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public XComponent ParseXComponent(InputStream arg0, Class<?> arg1, ReportWriter arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_PARSEXCOMPONENT_4, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent parseXComponent(java.io.InputStream, java.lang.Class<?>, java.lang.String, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public XComponent ParseXComponent(InputStream arg0, Class<?> arg1, String arg2, ReportWriter arg3)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                builder.Add(arg3);
                var res = SendRequestWithResponse(new Request(FUNCTION_PARSEXCOMPONENT_5, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent parseXComponent(org.w3c.dom.Node, java.lang.Class<?>, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public XComponent ParseXComponent(Node arg0, Class<?> arg1, ReportWriter arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_PARSEXCOMPONENT_6, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract java.lang.Object jparse(java.lang.String, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public Object Jparse(String arg0, ReportWriter arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSE_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Object(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract java.lang.Object jparse(java.io.File, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public Object Jparse(File arg0, ReportWriter arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSE_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Object(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract java.lang.Object jparse(java.net.URL, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public Object Jparse(URL arg0, ReportWriter arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSE_3, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Object(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract java.lang.Object jparse(java.io.InputStream, java.lang.String, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public Object Jparse(InputStream arg0, String arg1, ReportWriter arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSE_4, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Object(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract java.lang.Object jparse(org.w3c.dom.Node, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public Object Jparse(Node arg0, ReportWriter arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSE_5, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Object(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract java.lang.Object jparse(java.lang.Object, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public Object Jparse(Object arg0, ReportWriter arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSE_6, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Object(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent jparseXComponent(java.lang.String, java.lang.Class<?>, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public XComponent JparseXComponent(String arg0, Class<?> arg1, ReportWriter arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSEXCOMPONENT_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent jparseXComponent(java.net.URL, java.lang.Class<?>, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public XComponent JparseXComponent(URL arg0, Class<?> arg1, ReportWriter arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSEXCOMPONENT_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent jparseXComponent(java.io.InputStream, java.lang.String, java.lang.Class<?>, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public XComponent JparseXComponent(InputStream arg0, String arg1, Class<?> arg2, ReportWriter arg3)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                builder.Add(arg3);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSEXCOMPONENT_3, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent jparseXComponent(java.io.File, java.lang.Class<?>, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public XComponent JparseXComponent(File arg0, Class<?> arg1, ReportWriter arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSEXCOMPONENT_4, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent jparseXComponent(org.w3c.dom.Node, java.lang.Class<?>, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public XComponent JparseXComponent(Node arg0, Class<?> arg1, ReportWriter arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSEXCOMPONENT_5, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.component.XComponent jparseXComponent(java.lang.Object, java.lang.Class<?>, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        public XComponent JparseXComponent(Object arg0, Class<?> arg1, ReportWriter arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_JPARSEXCOMPONENT_6, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XComponent(reader.ReadInt32(), _client);
                }
            }
        } */

        // Autogenerated method
        //  public abstract org.xdef.XDOutput getStdOut();
        /// <summary>get StdOut.</summary>
		/// <returns>std out XDOutput.</returns>
        public XDOutput GetStdOut()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETSTDOUT, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XDOutput(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.XDOutput getStdErr();
        /// <summary>get StdErr.</summary>
		/// <returns>std err XDOutput.</returns>
        public XDOutput GetStdErr()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETSTDERR, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XDOutput(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.XDInput getStdIn();
        /// <summary>get StdIn.</summary>
		/// <returns>std in XDInput.</returns>
        public XDInput GetStdIn()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETSTDIN, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XDInput(reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract void setStreamWriter(java.io.OutputStream, java.lang.String, boolean) throws java.io.IOException;
        /// <summary>Set XML writer.</summary>
		/// <param name="output">output stream.</param>
		/// <param name="encoding">encoding of output.</param>
		/// <param name="writeDocumentHeader">
		/// if true full document is written, otherwise
		/// only root element.
		/// </param>
		/// <exception cref="System.IO.IOException">if an error occurs.</exception>
        public void SetStreamWriter(Stream output, string encoding, bool writeDocumentHeader)
        {
            if (!output.CanWrite) throw new ArgumentException("Stream not writable");
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(_client.RegisterLocalObject(new RemoteStreamWrapper(_client, output)));
                builder.Add(encoding);
                builder.Add(writeDocumentHeader);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETSTREAMWRITER_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }
        /*
        // Autogenerated method
        //  public abstract void setStreamWriter(java.io.Writer, java.lang.String, boolean);
        public void SetStreamWriter(Writer arg0, String arg1, boolean arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETSTREAMWRITER_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract void setStreamWriter(org.xdef.XDXmlOutStream);
        public void SetStreamWriter(XDXmlOutStream arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETSTREAMWRITER_3, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract void setStdOut(java.io.PrintStream);

        public void SetStdOut(PrintStream arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETSTDOUT_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract void setStdOut(java.io.Writer);
        public void SetStdOut(Writer arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETSTDOUT_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }*/

        // Autogenerated method
        //  public abstract void setStdIn(java.io.InputStream);
        /// <summary>Set standard input stream.</summary>
		/// <param name="input">InputStream object.</param>
        public void SetStdIn(Stream input)
        {
            if (!input.CanRead) throw new ArgumentException("Stream no readable");
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(_client.RegisterLocalObject(new RemoteStreamWrapper(_client, input)));
                var res = SendRequestWithResponse(new Request(FUNCTION_SETSTDIN_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract void setStdOut(org.xdef.XDOutput);
        /// <summary>Set standard output stream.</summary>
		/// <param name="output">XDOutput object.</param>
        public void SetStdOut(XDOutput output)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(output.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETSTDOUT_3, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract void setStdIn(org.xdef.XDInput);
        /// <summary>Set standard input stream.</summary>
		/// <param name="input">XDInput object.</param>
        public void SetStdIn(XDInput input)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(input.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETSTDIN_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }
        /*
        // Autogenerated method
        //  public abstract void setDebugger(org.xdef.XDDebug);
        public void SetDebugger(XDDebug arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETDEBUGGER, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.XDDebug getDebugger();
        public XDDebug GetDebugger()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETDEBUGGER, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XDDebug(reader.ReadInt32(), _client);
                }
            }
        }*/

        // Autogenerated method
        //  public abstract void setDebug(boolean);
        /// <summary>Set debugging mode.</summary>
		/// <param name="debug">debugging mode.</param>
        public void SetDebug(bool debug)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(debug);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETDEBUG, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract boolean isDebug();
        /// <summary>Check debugging mode is set ON.</summary>
		/// <returns>value of debugging mode.</returns>
        public bool IsDebug()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_ISDEBUG, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadBoolean();
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.proc.XXElement prepareRootXXElementNS(java.lang.String, java.lang.String, boolean);
        /*
        public XXElement PrepareRootXXElementNS(String arg0, String arg1, bool arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = SendRequestWithResponse(new Request(FUNCTION_PREPAREROOTXXELEMENTNS, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XXElement(reader.ReadInt32(), _client);
                }
            }
        }*/

        // Autogenerated method
        //  public abstract org.xdef.proc.XXElement prepareRootXXElement(java.lang.String, boolean);
        /*
        public XXElement PrepareRootXXElement(String arg0, boolean arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = SendRequestWithResponse(new Request(FUNCTION_PREPAREROOTXXELEMENT, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new XXElement(reader.ReadInt32(), _client);
                }
            }
        }*/

        // Autogenerated method
        //  public abstract java.util.Properties getImplProperties();
        /// <summary>Get implementation properties of X-definition.</summary>
		/// <returns>the implementation properties of X-definition.</returns>
        public Utils.Properties GetImplProperties()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETIMPLPROPERTIES, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return Utils.Properties.Deserialize(reader);
                }
            }
        }

        // Autogenerated method
        //  public abstract java.lang.String getImplProperty(java.lang.String);
        /// <summary>Get implementation property of X-definition.</summary>
		/// <param name="name">The name of property.</param>
		/// <returns>the value of implementation property from root X-definition.</returns>
        public String GetImplProperty(string name)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(name);
                var res = SendRequestWithResponse(new Request(FUNCTION_GETIMPLPROPERTY, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        //  public abstract boolean isLegalDate(org.xdef.sys.SDatetime);
        /*
        public bool IsLegalDate(SDatetime arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_ISLEGALDATE, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadBoolean();
                }
            }
        }*/

        // Autogenerated method
        //  public abstract int getMinYear();
        /// <summary>Get minimum valid year of date.</summary>
		/// <returns>minimum valid year (int.MinValue if not set).</returns>
        public int GetMinYear()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETMINYEAR, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadInt32();
                }
            }
        }

        // Autogenerated method
        //  public abstract void setMinYear(int);
        /// <summary>Set minimum valid year of date (or int.MinValue is not set).</summary>
		/// <param name="x">minimum valid year.</param>
        public void SetMinYear(int x)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(x);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETMINYEAR, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract int getMaxYear();
        /// <summary>Get maximum valid year of date (or Integer.MIN if not set).</summary>
		/// <returns>maximum valid year (int.MinValue if not set).</returns>
        public int GetMaxYear()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETMAXYEAR, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadInt32();
                }
            }
        }

        // Autogenerated method
        //  public abstract void setMaxYear(int);
        /// <summary>Set maximum valid year of date (or int.MinValue is not set).</summary>
		/// <param name="x">maximum valid year.</param>
        public void SetMaxYear(int x)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(x);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETMAXYEAR, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract org.xdef.sys.SDatetime[] getSpecialDates();
        /*
        public SDatetime[] GetSpecialDates()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETSPECIALDATES, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new SDatetime[](reader.ReadInt32(), _client);
                }
            }
        }

        // Autogenerated method
        //  public abstract void setSpecialDates(org.xdef.sys.SDatetime[]);
        public void SetSpecialDates(SDatetime[] arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETSPECIALDATES, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }*/

        // Autogenerated method
        //  public abstract void checkDateLegal(boolean);
        /// <summary>Set if year of date will be checked for interval minYear..maxYear.</summary>
		/// <param name="x">if true year of date will be checked.</param>
        public void CheckDateLegal(bool x)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(x);
                var res = SendRequestWithResponse(new Request(FUNCTION_CHECKDATELEGAL, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract void printReports(java.io.PrintStream);
        /*
        public void PrintReports(PrintStream arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_PRINTREPORTS, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }*/

        // Autogenerated method
        //  public abstract java.lang.String getLexiconLanguage();
        /// <summary>Get actual source language used for lexicon.</summary>
		/// <returns>
		/// string with actual language or return null if lexicon is not
		/// specified  or if language is not specified.
		/// </returns>
        public String GetLexiconLanguage()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETLEXICONLANGUAGE, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        //  public abstract void setLexiconLanguage(java.lang.String) throws org.xdef.sys.SRuntimeException;
        /// <summary>Set actual source language used for lexicon.</summary>
		/// <param name="language">string with language or null.</param>
		/// <exception cref="RemoteCallException">
		/// if lexicon not specified
		/// or if language is not specified.
		/// </exception>
        public void SetLexiconLanguage(string language)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(language);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETLEXICONLANGUAGE, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xtranslate(java.lang.String, java.lang.String, java.lang.String, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>
		/// Translate the input element from the source language to the destination
		/// language according to lexicon.
		/// </summary>
		/// <param name="elem">
		/// path to the source element or the string
		/// with element.
		/// </param>
		/// <param name="sourceLanguage">name of source language.</param>
		/// <param name="destLanguage">name of destination language.</param>
		/// <param name="reporter">the reporter where to write errors or null.</param>
		/// <returns>element converted to the destination language.</returns>
		/// <exception cref="RemoteCallException">if an error occurs.</exception>
        public XElement Xtranslate(string elem, string sourceLanguage, string destLanguage, ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(elem);
                builder.Add(sourceLanguage);
                builder.Add(destLanguage);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XTRANSLATE_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

        // Autogenerated method
        //  public abstract org.w3c.dom.Element xtranslate(org.w3c.dom.Element, java.lang.String, java.lang.String, org.xdef.sys.ReportWriter) throws org.xdef.sys.SRuntimeException;
        /// <summary>
		/// Translate the input element from the source language to the destination
		/// language according to lexicon.
		/// </summary>
		/// <param name="elem">the element in the source language.</param>
		/// <param name="sourceLanguage">name of source language.</param>
		/// <param name="destLanguage">name of destination language.</param>
		/// <param name="reporter">the reporter where to write errors or null.</param>
		/// <returns>element converted to the destination language.</returns>
		/// <exception cref="RemoteCallException">if an error occurs.</exception>
        public XElement Xtranslate(XElement elem, String sourceLanguage, String destLanguage, ReportWriter reporter)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(elem);
                builder.Add(sourceLanguage);
                builder.Add(destLanguage);
                builder.Add(reporter.ObjectId);
                var res = SendRequestWithResponse(new Request(FUNCTION_XTRANSLATE_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }

    }
}
