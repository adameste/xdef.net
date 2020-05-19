using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net
{
    public static class XDConstants
    {
        /// <summary>Property defines debug mode (default is false).</summary>
        public const string XDPROPERTY_DEBUG = "xdef_debug";

        /// <summary>Value "true" of property "xdef_debug"</summary>
        public const string XDPROPERTYVALUE_DEBUG_TRUE = "true";

        /// <summary>Value "false" of property "xdef_debug"</summary>
        public const string XDPROPERTYVALUE_DEBUG_FALSE = "false";

        /// <summary>Value "showResult" of property "xdef_debug"</summary>
        public const string XDPROPERTYVALUE_DEBUG_SHOWRESULT = "showResult";

        /// <summary>Property defines stream used for debug output (default is stdOut).</summary>
        public const string XDPROPERTY_DEBUG_OUT = "xdef_debug_out";

        /// <summary>Property defines stream used for debug input (default is stdIn).</summary>
        public const string XDPROPERTY_DEBUG_IN = "xdef_debug_in";

        /// <summary>Property defines debug editor class name.</summary>
        public const string XDPROPERTY_DEBUG_EDITOR = "xdef_debugeditor";

        /// <summary>Property defines X-definition editor class name.</summary>
        public const string XDPROPERTY_XDEF_EDITOR = "xdef_editor";

        /// <summary>Property defines X-definition external editor class name.</summary>
        public const string XDPROPERTY_XDEF_EXTEDITOR = "xdef_exteditor";

        /// <summary>Property defines if XML DOCTYPE is permitted (default is "true").</summary>
        public const string XDPROPERTY_DOCTYPE = "xdef_doctype";

        /// <summary>Value "false" of property "xdef_doctype"</summary>
        public const string XDPROPERTYVALUE_DOCTYPE_FALSE = "false";

        /// <summary>Value "true" of property "xdef_doctype"</summary>
        public const string XDPROPERTYVALUE_DOCTYPE_TRUE = "true";

        /// <summary>Set the XML parser will generate detailed location in reports.</summary>
        public const string XDPROPERTY_LOCATIONDETAILS = "xdef_locationsdetails";

        /// <summary>Value "false" of property "xdef_locationsdetails" (default).</summary>
        public const string XDPROPERTYVALUE_LOCATIONDETAILS_FALSE = "false";

        /// <summary>Value "true" of property "xdef_locationsdetails"</summary>
        public const string XDPROPERTYVALUE_LOCATIONDETAILS_TRUE = "true";

        /// <summary>Property defines if XML include is permitted (default is "true").</summary>
        public const string XDPROPERTY_XINCLUDE = "xdef_xinclude";

        /// <summary>Value "false" of property "xdef_xinclude"</summary>
        public const string XDPROPERTYVALUE_XINCLUDE_FALSE = "false";

        /// <summary>Value "true" of property "xdef_xinclude (default)."</summary>
        public const string XDPROPERTYVALUE_XINCLUDE_TRUE = "true";

        /// <summary>Property warning messages are checked {thrown} (default is "false")</summary>
        public const string XDPROPERTY_WARNINGS = "xdef_warnings";

        /// <summary>Value "true" of property "xdef_warnings"</summary>
        public const string XDPROPERTYVALUE_WARNINGS_TRUE = "true";

        /// <summary>Value "false" of property "xdef_warnings"</summary>
        public const string XDPROPERTYVALUE_WARNINGS_FALSE = "false";

        /// <summary>Property defines debug mode (default is false).</summary>
        public const string XDPROPERTY_DISPLAY = "xdef_display";

        /// <summary>Value "true" of property "xdef_display"</summary>
        public const string XDPROPERTYVALUE_DISPLAY_TRUE = "true";

        /// <summary>Value "errors" of property "xdef_display"</summary>
        public const string XDPROPERTYVALUE_DISPLAY_ERRORS = "errors";

        /// <summary>Value "false" of property "xdef_display"</summary>
        public const string XDPROPERTYVALUE_DISPLAY_FALSE = "false";

        /// <summary>Property defines minimal valid year of date (default is no minimum).</summary>
        public const string XDPROPERTY_MINYEAR = "xdef_minyear";

        /// <summary>Property defines maximal valid year of date (default is no maximum).</summary>
        public const string XDPROPERTY_MAXYEAR = "xdef_maxyear";

        /// <summary>Property defines legal values of dates if year is out of range.</summary>
        public const string XDPROPERTY_SPECDATES = "xdef_specdates";

        /// <summary>
        /// Property defines if unresolved external methods are reported (used
        /// for syntax checking of X-definition (default is "false").
        /// </summary>
        public const string XDPROPERTY_IGNORE_UNDEF_EXT = "xdef_ignoreUnresolvedExternals";

        /// <summary>Value "true" of property "xdef_ignoreUnresolvedExternals"</summary>
        public const string XDPROPERTYVALUE_IGNORE_UNDEF_EXT_TRUE = "true";

        /// <summary>Value "false" of property "xdef_ignoreUnresolvedExternals"</summary>
        public const string XDPROPERTYVALUE_IGNORE_UNDEF_EXT_FALSE = "false";

        /// <summary>Prefix of property names for setting of message table files.</summary>
        public const string XDPROPERTY_MESSAGES = "xdef_msg_";

        /// <summary>Name of property for setting language of messages.</summary>
        public const string XDPROPERTY_MSGLANGUAGE = "xdef_language";

        /// <summary>URI of w3c XLink</summary>
        public const string XLINK_NS_URI = "http://www.w3.org/1999/xlink";

        /// <summary>URI of w3c XInclude</summary>
        public const string XINCLUDE_NS_URI = "http://www.w3.org/2001/XInclude";

        /// <summary>Recommended namespace prefix used for X-definition nodes.</summary>
        public const string XDEF_NS_PREFIX = "xd";

        /// <summary>URI of X-definition 2.0.</summary>
        public const string XDEF20_NS_URI = "http://www.syntea.cz/xdef/2.0";

        /// <summary>URI of X-definition 3.1.</summary>
        public const string XDEF31_NS_URI = "http://www.syntea.cz/xdef/3.1";

        /// <summary>URI of X-definition 3.2.</summary>
        public const string XDEF32_NS_URI = "http://www.xdef.org/xdef/3.2";

        /// <summary>URI of X-definition 4.0.</summary>
        public const string XDEF40_NS_URI = "http://www.xdef.org/xdef/4.0";

        /// <summary>The namespace URI for X-definition instance.</summary>
        public const string XDEF_INSTANCE_NS_URI = "http://www.xdef.org/xdef/instance";

        /// <summary>Recommended namespace prefix used for JSON/XML conversion nodes.</summary>
        public const string JSON_NS_PREFIX = "js";

        /// <summary>URI of JSON/XML X-definition conversion.</summary>
        public const string JSON_NS_URI = "http://www.xdef.org/json/3.2";

        /// <summary>URI of JSON/XML W3C conversion (https://www.w3.org/TR/xslt-30/#json).</summary>
        public const string JSON_NS_URI_W3C = "http://www.w3.org/2005/xpath-functions";

        /// <summary>Platform-dependent line separator (newline characters: LF, CR LF, etc.</summary>
        public static readonly string LINE_SEPARATOR = Environment.NewLine;
    }
}
