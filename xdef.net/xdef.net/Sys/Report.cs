﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using xdef.net.Connection;
using xdef.net.Utils;

namespace xdef.net.Sys
{
    public sealed class Report : RemoteObject
    {
        private const int FUNCTION_GENMODIFICATION = 1;
        private const int FUNCTION_TOSTRING_1 = 2;
        private const int FUNCTION_TOSTRING_2 = 3;
        private const int FUNCTION_GETTYPE = 4;
        private const int FUNCTION_GETMSGID = 5;
        private const int FUNCTION_GETTEXT = 6;
        private const int FUNCTION_SETTEXT = 7;
        private const int FUNCTION_GETTIMESTAMP = 8;
        private const int FUNCTION_SETTIMESTAMP_1 = 9;
        private const int FUNCTION_SETTIMESTAMP_2 = 10;
        private const int FUNCTION_GETMODIFICATION = 11;
        private const int FUNCTION_SETMODIFICATION = 12;
        private const int FUNCTION_GETPARAMETER = 13;
        private const int FUNCTION_SETPARAMETER = 14;
        private const int FUNCTION_GETLOCALIZEDTEXT_1 = 15;
        private const int FUNCTION_GETLOCALIZEDTEXT_2 = 16;
        private const int FUNCTION_TOXMLSTRING = 17;
        private const int FUNCTION_TOXMLELEMENT = 18;
        private const int FUNCTION_SETLANGUAGE = 19;
        private const int FUNCTION_AUDIT_1 = 20;
        private const int FUNCTION_FATAL_1 = 21;
        private const int FUNCTION_ERROR_1 = 22;
        private const int FUNCTION_LIGHTERROR_1 = 23;
        private const int FUNCTION_WARNING_1 = 24;
        private const int FUNCTION_MESSAGE_1 = 25;
        private const int FUNCTION_INFO_1 = 26;
        private const int FUNCTION_STRING_1 = 27;
        private const int FUNCTION_TEXT_1 = 28;
        private const int FUNCTION_GETRAWREPORTTEXT_1 = 29;
        private const int FUNCTION_GETREPORTTEXT_1 = 30;
        private const int FUNCTION_GETREPORTTEXT_2 = 31;
        private const int FUNCTION_GETLOCALIZEDTEXT_3 = 32;
        private const int FUNCTION_GETREPORTPARAMNAMES = 33;
        private const int FUNCTION_GETLOCALIZEDTEXT_4 = 34;
        private const int FUNCTION_AUDIT_2 = 35;
        private const int FUNCTION_FATAL_2 = 36;
        private const int FUNCTION_ERROR_2 = 37;
        private const int FUNCTION_LIGHTERROR_2 = 38;
        private const int FUNCTION_WARNING_2 = 39;
        private const int FUNCTION_MESSAGE_2 = 40;
        private const int FUNCTION_INFO_2 = 41;
        private const int FUNCTION_STRING_2 = 42;
        private const int FUNCTION_TEXT_2 = 43;
        private const int FUNCTION_GETREPORTID = 44;
        private const int FUNCTION_GETRAWREPORTTEXT_2 = 45;
        private const int FUNCTION_GETREPORTTEXT_3 = 46;
        private const int FUNCTION_GETREPORTTEXT_4 = 47;
        private const int FUNCTION_BUILDINFO = 48;
        private const int FUNCTION_WRITEOBJ = 49;
        private const int FUNCTION_READOBJ = 50;

        /// <summary>Text report object (byte value of the character 'S').</summary>
		public const byte STRING = (byte)('S');

        /// <summary>Text report object (byte value of the character 'T').</summary>
        public const byte TEXT = (byte)('T');

        /// <summary>Audit report object (byte value of the character 'A').</summary>
        public const byte AUDIT = (byte)('A');

        /// <summary>Message report object (byte value of the character 'M').</summary>
        public const byte MESSAGE = (byte)('M');

        /// <summary>Info report object (byte value of the character 'I').</summary>
        public const byte INFO = (byte)('I');

        /// <summary>Warning report object (byte value of the character 'W').</summary>
        public const byte WARNING = (byte)('W');

        /// <summary>Light error report object (byte value of the character 'L').</summary>
        public const byte LIGHTERROR = (byte)('L');

        /// <summary>LightError report object (byte value of the character 'E').</summary>
        public const byte ERROR = (byte)('E');

        /// <summary>Fatal error report object (byte value of the character 'F').</summary>
        public const byte FATAL = (byte)('F');

        /// <summary>Exception report object (byte value of the character 'X').</summary>
        public const byte EXCEPTION = (byte)('X');

        /// <summary>Trace report object (byte value of the character 'D').</summary>
        public const byte TRACE = (byte)('D');

        /// <summary>Kill report object (byte value of the character 'K').</summary>
        public const byte KILL = (byte)('K');

        /// <summary>Undefined report object (byte value of the character 'U').</summary>
        public const byte UNDEF = (byte)('U');


        private static Lazy<int> _staticObjectId = new Lazy<int>(() =>
        {
            var res = XD.Instance.Client.SendRequestWithResponse(new CreateObjectRequest("StaticReport"));
            using (var reader = res.Reader)
                return reader.ReadInt32();
        });

        internal Report(int objectId, Client client) : base(objectId, client)
        {
        }


        // Autogenerated method
        // public static java.lang.String genModification(java.lang.Object...)
        //public static string GenModification(Object... arg0)
        //{
        //    using (var builder = new BigEndianDataBuilder())
        //    {
        //        // Serialize args here
        //        builder.Add(arg0);
        //        var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_GENMODIFICATION, builder.Build(), _staticObjectId.Value));
        //        using (var reader = res.Reader)
        //        {
        //            return reader.ReadString();
        //        }
        //    }
        //}

        // Autogenerated method
        // public java.lang.String toString()
        public string ToString()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_TOSTRING_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public java.lang.String toString(java.lang.String)
        public string ToString(string arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_TOSTRING_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public final byte getType()
        public byte GetType()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETTYPE, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadByte();
                }
            }
        }

        // Autogenerated method
        // public final java.lang.String getMsgID()
        public string GetMsgID()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETMSGID, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public final java.lang.String getText()
        public string GetText()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETTEXT, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public final void setText(java.lang.String)
        public void SetText(string arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETTEXT, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        // public final long getTimestamp()
        public long GetTimestamp()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETTIMESTAMP, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadInt64();
                }
            }
        }

        // Autogenerated method
        // public final void setTimestamp()
        public void SetTimestamp()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_SETTIMESTAMP_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        // public final void setTimestamp(long)
        public void SetTimestamp(long arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETTIMESTAMP_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        // public final java.lang.String getModification()
        public string GetModification()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETMODIFICATION, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public final void setModification(java.lang.String)
        public void SetModification(string arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETMODIFICATION, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        // public final java.lang.String getParameter(java.lang.String)
        public string GetParameter(string arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_GETPARAMETER, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public final void setParameter(java.lang.String, java.lang.String)
        public void SetParameter(string arg0, string arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = SendRequestWithResponse(new Request(FUNCTION_SETPARAMETER, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return;
                }
            }
        }

        // Autogenerated method
        // public final java.lang.String getLocalizedText()
        public string GetLocalizedText()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_GETLOCALIZEDTEXT_1, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public final java.lang.String getLocalizedText(java.lang.String)
        public string GetLocalizedText(string arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_GETLOCALIZEDTEXT_2, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public final java.lang.String toXmlString()
        public string ToXmlString()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = SendRequestWithResponse(new Request(FUNCTION_TOXMLSTRING, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public final org.w3c.dom.Element toXmlElement(org.w3c.dom.Document)
        public XElement ToXmlElement(XDocument arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = SendRequestWithResponse(new Request(FUNCTION_TOXMLELEMENT, builder.Build(), ObjectId));
                using (var reader = res.Reader)
                {
                    return XElement.Parse(reader.ReadString());
                }
            }
        }
        /*
        // Autogenerated method
        // public static java.lang.Object setLanguage(java.lang.String)
        public static Object SetLanguage(string arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_SETLANGUAGE, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Object(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report audit(java.lang.String, java.lang.String, java.lang.Object...)
        public static Report Audit(string arg0, string arg1, Object... arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_AUDIT_1, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report fatal(java.lang.String, java.lang.String, java.lang.Object...)
        public static Report Fatal(string arg0, string arg1, Object... arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_FATAL_1, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report error(java.lang.String, java.lang.String, java.lang.Object...)
        public static Report Error(string arg0, string arg1, Object... arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_ERROR_1, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report lightError(java.lang.String, java.lang.String, java.lang.Object...)
        public static Report LightError(string arg0, string arg1, Object... arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_LIGHTERROR_1, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report warning(java.lang.String, java.lang.String, java.lang.Object...)
        public static Report Warning(string arg0, string arg1, Object... arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_WARNING_1, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report message(java.lang.String, java.lang.String, java.lang.Object...)
        public static Report Message(string arg0, string arg1, Object... arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_MESSAGE_1, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report info(java.lang.String, java.lang.String, java.lang.Object...)
        public static Report Info(string arg0, string arg1, Object... arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_INFO_1, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report string(java.lang.String, java.lang.String, java.lang.Object...)
        public static Report String(string arg0, string arg1, Object... arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_STRING_1, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report text(java.lang.String, java.lang.String, java.lang.Object...)
        public static Report Text(string arg0, string arg1, Object... arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_TEXT_1, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }
        */
        // Autogenerated method
        // public static java.lang.String getRawReportText(java.lang.String, java.lang.String)
        public static string GetRawReportText(string arg0, string arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_GETRAWREPORTTEXT_1, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public static java.lang.String getReportText(java.lang.String, java.lang.String)
        public static string GetReportText(string arg0, string arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_GETREPORTTEXT_1, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public static java.lang.String getReportText(java.lang.String)
        public static string GetReportText(string arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_GETREPORTTEXT_2, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public static java.lang.String getLocalizedText(java.lang.String, java.lang.String, java.lang.String, java.lang.String)
        public static string GetLocalizedText(string arg0, string arg1, string arg2, string arg3)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                builder.Add(arg3);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_GETLOCALIZEDTEXT_3, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public static java.lang.String[] getReportParamNames(java.lang.String, java.lang.String)
        public static IEnumerable<string> GetReportParamNames(string arg0, string arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_GETREPORTPARAMNAMES, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    return reader.ReadStringArray();
                }
            }
        }

        // Autogenerated method
        // public static java.lang.String getLocalizedText(long, java.lang.String, java.lang.String)
        public static string GetLocalizedText(long arg0, string arg1, string arg2)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                builder.Add(arg2);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_GETLOCALIZEDTEXT_4, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report audit(long, java.lang.Object...)
        /*
        public static Report Audit(long arg0, Object... arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_AUDIT_2, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report fatal(long, java.lang.Object...)
        public static Report Fatal(long arg0, Object... arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_FATAL_2, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report error(long, java.lang.Object...)
        public static Report Error(long arg0, Object... arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_ERROR_2, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report lightError(long, java.lang.Object...)
        public static Report LightError(long arg0, Object... arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_LIGHTERROR_2, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report warning(long, java.lang.Object...)
        public static Report Warning(long arg0, Object... arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_WARNING_2, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report message(long, java.lang.Object...)
        public static Report Message(long arg0, Object... arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_MESSAGE_2, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report info(long, java.lang.Object...)
        public static Report Info(long arg0, Object... arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_INFO_2, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report string(long, java.lang.Object...)
        public static Report String(long arg0, Object... arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_STRING_2, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report text(long, java.lang.Object...)
        public static Report Text(long arg0, Object... arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_TEXT_2, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }
        */

        // Autogenerated method
        // public static java.lang.String getReportID(long)
        public static string GetReportID(long arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_GETREPORTID, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public static java.lang.String getRawReportText(long, java.lang.String)
        public static string GetRawReportText(long arg0, string arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_GETRAWREPORTTEXT_2, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public static java.lang.String getReportText(long, java.lang.String)
        public static string GetReportText(long arg0, string arg1)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                builder.Add(arg1);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_GETREPORTTEXT_3, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public static java.lang.String getReportText(long)
        public static string GetReportText(long arg0)
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                builder.Add(arg0);
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_GETREPORTTEXT_4, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    return reader.ReadString();
                }
            }
        }

        // Autogenerated method
        // public static org.xdef.sys.Report buildInfo()
        public static Report BuildInfo()
        {
            using (var builder = new BigEndianDataBuilder())
            {
                // Serialize args here
                var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_BUILDINFO, builder.Build(), _staticObjectId.Value));
                using (var reader = res.Reader)
                {
                    // Read response here
                    return new Report(reader.ReadInt32(), XD.Instance.Client);
                }
            }
        }

        // Autogenerated method
        // public void writeObj(org.xdef.sys.SObjectWriter)
        //public void WriteObj(SObjectWriter arg0)
        //{
        //    using (var builder = new BigEndianDataBuilder())
        //    {
        //        // Serialize args here
        //        builder.Add(arg0);
        //        var res = SendRequestWithResponse(new Request(FUNCTION_WRITEOBJ, builder.Build(), ObjectId));
        //        using (var reader = res.Reader)
        //        {
        //            return;
        //        }
        //    }
        //}

        // Autogenerated method
        // public static org.xdef.sys.Report readObj(org.xdef.sys.SObjectReader)
        //public static Report ReadObj(SObjectReader arg0)
        //{
        //    using (var builder = new BigEndianDataBuilder())
        //    {
        //        // Serialize args here
        //        builder.Add(arg0);
        //        var res = XD.Instance.Client.SendRequestWithResponse(new Request(FUNCTION_READOBJ, builder.Build(), _staticObjectId.Value));
        //        using (var reader = res.Reader)
        //        {
        //            // Read response here
        //            return new Report(reader.ReadInt32(), XD.Instance.Client);
        //        }
        //    }
        //}
    }
}
