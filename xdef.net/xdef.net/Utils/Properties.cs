using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Connection;

namespace xdef.net.Utils
{
    public class Properties : Dictionary<string, string>, IBinarySerializable
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

        public static Properties Deserialize(BigEndianBinaryReader reader)
        {
            var cnt = reader.ReadInt32();
            var res = new Properties();
            for(int i = 0; i < cnt; i++)
            {
                var key = reader.ReadString();
                var value = reader.ReadString();
                res[key] = value;
            }
            return res;
        }
    }
}
