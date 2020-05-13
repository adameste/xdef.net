using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Connection;

namespace xdef.net.Utils
{
    public class Properties : Dictionary<string, string>, IBinaryWriterSerializable
    {
        public void Serialize(BigEndianBinaryWriter writer)
        {
            writer.Write(this.Count);
            foreach (var it in this)
            {
                writer.Write(it.Key);
                writer.Write(it.Value);
            }
        }
    }
}
