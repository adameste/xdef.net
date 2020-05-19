using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Connection;
using xdef.net.Utils;

namespace xdef.net
{
    public class XDNamedValue : XDValue
    {
        public string Name { get; }
        public object Value { get; set; }

        public XDNamedValue(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public void Serialize(BigEndianBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
