using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace xdef.net.Utils
{
    public class BigEndianBinaryReader : BinaryReader
    {
        private readonly Encoding _encoding;

        public BigEndianBinaryReader(Stream input) : base(input, Encoding.UTF8)
        {
        }

        public BigEndianBinaryReader(Stream input, Encoding encoding) : base(input, encoding)
        {
            _encoding = encoding;
        }

        public BigEndianBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
            _encoding = encoding;
        }

        public override int ReadInt32()
        {
            var bytes = ReadBytes(4);
            return BitConverter.ToInt32(bytes.Reverse().ToArray(), 0);
        }
        public override double ReadDouble()
        {
            var bytes = ReadBytes(8);
            return BitConverter.ToDouble(bytes.Reverse().ToArray(), 0);
        }
        public override short ReadInt16()
        {
            var bytes = ReadBytes(2);
            return BitConverter.ToInt16(bytes.Reverse().ToArray(), 0);
        }
        public override uint ReadUInt32()
        {
            var bytes = ReadBytes(4);
            return BitConverter.ToUInt32(bytes.Reverse().ToArray(), 0);
        }
        public override ushort ReadUInt16()
        {
            var bytes = ReadBytes(2);
            return BitConverter.ToUInt16(bytes.Reverse().ToArray(), 0);
        }
        public override long ReadInt64()
        {
            var bytes = ReadBytes(8);
            return BitConverter.ToInt64(bytes.Reverse().ToArray(), 0);
        }
        public override ulong ReadUInt64()
        {
            var bytes = ReadBytes(8);
            return BitConverter.ToUInt64(bytes.Reverse().ToArray(), 0);
        }
        public override float ReadSingle()
        {
            var bytes = ReadBytes(4);
            return BitConverter.ToSingle(bytes.Reverse().ToArray(), 0);
        }
        public override string ReadString()
        {
            var payload = ReadInt32();
            return _encoding.GetString(ReadBytes(payload));
        }

        internal IEnumerable<string> ReadStringArray()
        {
            var cnt = ReadInt32();
            string[] arr = new string[cnt];
            for (int i = 0; i < cnt; i++)
                arr[i] = ReadString();
            return arr;
        }
    }
}
