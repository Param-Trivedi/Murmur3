using System;
using System.Collections.Generic;
using System.Text;

namespace Murmur3_128bit
{
    public static class HelperHash
    {
        public static ulong RotateLeft(this ulong original, int bits)
        {
            return (original << bits) | (original >> (64 - bits));
        }

        public static ulong RotateRight(this ulong original, int bits)
        {
            return (original >> bits) | (original << (64 - bits));
        }

        public static ulong RotateLeft32(this ulong original, int bits)
        {
            return (original << bits) | (original >> (32 - bits));
        }

        public static ulong RotateRight32(this ulong original, int bits)
        {
            return (original >> bits) | (original << (32 - bits));
        }

        public static ulong GetUInt64(this byte[] bb, int pos)
        {
            return BitConverter.ToUInt64(bb, pos);
        }

        public static ulong GetUInt32(this byte[] bb, int pos)
        {
            return BitConverter.ToUInt32(bb, pos);
        }
    }
}
