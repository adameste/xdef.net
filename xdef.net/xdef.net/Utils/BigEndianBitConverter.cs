using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xdef.net.Utils
{
    public static class BigEndianBitConverter
    {
        public static byte[] GetBytes(bool value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }
        public static byte[] GetBytes(char value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }
        public static byte[] GetBytes(double value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }
        public static byte[] GetBytes(short value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }
        public static byte[] GetBytes(int value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }
        public static byte[] GetBytes(long value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }
        public static byte[] GetBytes(float value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }
        public static byte[] GetBytes(ushort value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }
        public static byte[] GetBytes(uint value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }
        public static byte[] GetBytes(ulong value)
        {
            return BitConverter.GetBytes(value).Reverse().ToArray();
        }
        public static byte[] GetBytes(string value)
        {
            var data = Encoding.UTF8.GetBytes(value);
            return GetBytes(data.Length).Concat(data).ToArray();
        }

        public static bool ToBoolean(byte[] value, int startIndex)
        {
            return BitConverter.ToBoolean(value, startIndex);
        }
        public static char ToChar(byte[] value, int startIndex)
        {
            return BitConverter.ToChar(value, startIndex);
        }
        public static double ToDouble(byte[] value, int startIndex)
        {
            var bytes = value.Skip(startIndex).Take(8).Reverse().ToArray();
            return BitConverter.ToDouble(bytes, 0);
        }
        public static short ToInt16(byte[] value, int startIndex)
        {
            var bytes = value.Skip(startIndex).Take(2).Reverse().ToArray();
            return BitConverter.ToInt16(bytes, 0);
        }
        public static int ToInt32(byte[] value, int startIndex)
        {
            var bytes = value.Skip(startIndex).Take(4).Reverse().ToArray();
            return BitConverter.ToInt32(bytes, 0);
        }
        public static long ToInt64(byte[] value, int startIndex)
        {
            var bytes = value.Skip(startIndex).Take(8).Reverse().ToArray();
            return BitConverter.ToInt64(bytes, 0);
        }
        public static float ToSingle(byte[] value, int startIndex)
        {
            var bytes = value.Skip(startIndex).Take(4).Reverse().ToArray();
            return BitConverter.ToSingle(bytes, 0);
        }
        public static ushort ToUInt16(byte[] value, int startIndex)
        {
            var bytes = value.Skip(startIndex).Take(2).Reverse().ToArray();
            return BitConverter.ToUInt16(bytes, 0);
        }
        public static uint ToUInt32(byte[] value, int startIndex)
        {
            var bytes = value.Skip(startIndex).Take(4).Reverse().ToArray();
            return BitConverter.ToUInt32(bytes, 0);
        }
        public static ulong ToUInt64(byte[] value, int startIndex)
        {
            var bytes = value.Skip(startIndex).Take(8).Reverse().ToArray();
            return BitConverter.ToUInt64(bytes, 0);
        }

    }
}
