using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using xdef.net.Connection;

namespace xdef.net.Utils
{
    class BigEndianDataBuilder : IDisposable
    {
        private MemoryStream _stream;
        private BigEndianBinaryWriter _writer;

        public BigEndianDataBuilder()
        {
            _stream = new MemoryStream();
            _writer = new BigEndianBinaryWriter(_stream);
        }

        public BigEndianDataBuilder Add(string x)
        {
            _writer.Write(x);
            return this;
        }
        public BigEndianDataBuilder Add(int x)
        {
            _writer.Write(x);
            return this;
        }
        public BigEndianDataBuilder Add(double x)
        {
            _writer.Write(x);
            return this;
        }
        public BigEndianDataBuilder Add(bool x)
        {
            _writer.Write(x);
            return this;
        }
        public BigEndianDataBuilder Add(byte x)
        {
            _writer.Write(x);
            return this;
        }
        public BigEndianDataBuilder Add(IBinarySerializable x)
        {
            _writer.Write(x);
            return this;
        }

        public BigEndianDataBuilder Add(FilePath path)
        {
            _writer.Write(path.JavaPath);
            return this;
        }
        public BigEndianDataBuilder Add(Uri url)
        {
            _writer.Write(url.AbsoluteUri);
            return this;
        }
        public byte[] Build()
        {
            _writer.Flush();
            return _stream.ToArray();
        }

        public void Dispose()
        {
            _writer.Dispose();
        }

        internal BigEndianDataBuilder Add(XNode x)
        {
            _writer.Write(x.ToString(SaveOptions.DisableFormatting));
            return this;
        }

        internal void Add(XmlQualifiedName arg0)
        {
            Add(arg0.Namespace).Add(arg0.Name);
        }
    }
}
