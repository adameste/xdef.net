﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using xdef.net.Connection;

namespace xdef.net.Utils
{
    public class BigEndianBinaryWriter : BinaryWriter
    {
        private readonly Encoding _encoding;

        public BigEndianBinaryWriter(Stream output) : base(output, Encoding.UTF8)
        {
            _encoding = Encoding.UTF8;
        }

        public BigEndianBinaryWriter(Stream output, Encoding encoding) : base(output, encoding)
        {
            _encoding = encoding;
        }

        public BigEndianBinaryWriter(Stream output, Encoding encoding, bool leaveOpen) : base(output, encoding, leaveOpen)
        {
            _encoding = encoding;
        }

        protected BigEndianBinaryWriter()
        {
        }

        public override void Write(int value)
        {
            base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }
        public override void Write(double value)
        {
            base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }
        public override void Write(short value)
        {
            base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }
        public override void Write(uint value)
        {
            base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }
        public override void Write(ushort value)
        {
            base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }
        public override void Write(long value)
        {
            base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }
        public override void Write(ulong value)
        {
            base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }
        public override void Write(float value)
        {
            base.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }
        public override void Write(string value)
        {
            if (value == null) Write(0);
            else
            {
                var bytes = _encoding.GetBytes(value);
                Write(bytes.Length);
                Write(bytes);
            }
        }
        public void Write(IBinarySerializable value)
        {
            if (value == null) Write(0);
            else value.Serialize(this);
        }
    }
}
