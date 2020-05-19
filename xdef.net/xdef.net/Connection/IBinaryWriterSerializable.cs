using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Utils;

namespace xdef.net.Connection
{
    public interface IBinarySerializable
    {
        void Serialize(BigEndianBinaryWriter writer);
    }
}
