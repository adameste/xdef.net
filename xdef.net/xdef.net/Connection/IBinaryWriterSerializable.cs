using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Utils;

namespace xdef.net.Connection
{
    public interface IBinaryWriterSerializable
    {
        void Serialize(BigEndianBinaryWriter writer);
    }
}
