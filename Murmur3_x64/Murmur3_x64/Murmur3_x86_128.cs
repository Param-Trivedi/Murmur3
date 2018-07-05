using System;
using System.Collections.Generic;
using System.Text;

namespace Murmur3_128bit
{
    public class Murmur3_x86_128
    {
        public static ulong READ_SIZE = 16;

        private static ulong C1 = 0x239b961b;
        private static ulong C2 = 0xab0e9789;
        private static ulong C3 = 0x38b34ae5;
        private static ulong C4 = 0xa1e38b93;

        // Seed value used for hashing 
        private uint seed;

        // Length
        private ulong length;

        ulong hash1;
        ulong hash2;
        ulong hash3;
        ulong hash4;

        public Murmur3_x86_128(uint _Seed)
        {
            seed = _Seed;
            hash1 = seed;
            hash2 = seed;
            hash3 = seed;
            hash4 = seed;
        }


        private void ProcessBytes(byte[] bb)
        {
            this.length = 0L;

            int pos = 0;
            ulong remaining = (ulong)bb.Length;

            // read 128 bits, 16 bytes, 2 longs in eacy cycle
            while (remaining >= READ_SIZE)
            {
                ulong k1 = bb.GetUInt32(pos);
                pos += 4;

                ulong k2 = bb.GetUInt32(pos);
                pos += 4;

                ulong k3 = bb.GetUInt32(pos);
                pos += 4;

                ulong k4 = bb.GetUInt32(pos);
                pos += 4;

                length += READ_SIZE;
                remaining -= READ_SIZE;

                MixBody(k1, k2, k3, k4);

            }

             //if the input MOD 16 != 0
             if (remaining > 0)
               ProcessBytesRemaining(bb, remaining, pos);
        }

        private void ProcessBytesRemaining(byte[] bb, ulong remaining, int pos)
        {
            ulong k1 = 0;
            ulong k2 = 0;
            ulong k3 = 0;
            ulong k4 = 0;
            length += remaining;

            // little endian (x86) processing
            switch (remaining)
            {
                case 15:
                    k4 ^= (ulong)bb[pos + 14] << 16; // fall through
                    goto case 14;
                case 14:
                    k4 ^= (ulong)bb[pos + 13] << 8; // fall through
                    goto case 13;
                case 13:
                    k4 ^= (ulong)bb[pos + 12] << 0; // fall through
                    goto case 12;
                case 12:
                    k3 ^= (ulong)bb[pos + 11] << 24; // fall through
                    goto case 11;
                case 11:
                    k3 ^= (ulong)bb[pos + 10] << 16; // fall through
                    goto case 10;
                case 10:
                    k3 ^= (ulong)bb[pos + 9] << 8; // fall through
                    goto case 9;
                case 9:
                    k3 ^= (ulong)bb[pos + 8]; // fall through
                    goto case 8;
                case 8:
                    k2 ^= (ulong)bb[pos + 7] << 24;
                    break;
                case 7:
                    k2 ^= (ulong)bb[pos + 6] << 16; // fall through
                    goto case 6;
                case 6:
                    k2 ^= (ulong)bb[pos + 5] << 8; // fall through
                    goto case 5;
                case 5:
                    k2 ^= (ulong)bb[pos + 4] << 0; // fall through
                    goto case 4;
                case 4:
                    k1 ^= (ulong)bb[pos + 3] << 24; // fall through
                    goto case 3;
                case 3:
                    k1 ^= (ulong)bb[pos + 2] << 16; // fall through
                    goto case 2;
                case 2:
                    k1 ^= (ulong)bb[pos + 1] << 8; // fall through
                    goto case 1;
                case 1:
                    k1 ^= (ulong)bb[pos]; // fall through
                    break;
                default:
                    throw new Exception("Something went wrong with remaining bytes calculation.");
            }

            hash1 ^= MixKey1(k1);
            hash2 ^= MixKey2(k2);
            hash3 ^= MixKey3(k3);
            hash4 ^= MixKey4(k4);
        }

        public byte[] ComputeHash(byte[] bb)
        {
            ProcessBytes(bb);
            return Hash;
        }

        public byte[] Hash
        {
            get
            {
                hash1 ^= length;
                hash2 ^= length;
                hash3 ^= length;
                hash4 ^= length;

                hash1 += hash2;
                hash2 += hash3;
                hash3 += hash4;
                hash4 += hash1;

                hash1 = MixFinal(hash1);
                hash2 = MixFinal(hash2);
                hash3 = MixFinal(hash3);
                hash4 = MixFinal(hash4);

                hash1 += hash2;
                hash2 += hash3;
                hash3 += hash4;
                hash4 += hash1;

                var hash = new byte[READ_SIZE];

                Array.Copy(BitConverter.GetBytes(hash1), 0, hash, 0, 4);
                Array.Copy(BitConverter.GetBytes(hash2), 0, hash, 4, 4);
                Array.Copy(BitConverter.GetBytes(hash3), 0, hash, 8, 4);
                Array.Copy(BitConverter.GetBytes(hash4), 0, hash, 12, 4);


                return hash;
            }
        }

        private static ulong MixFinal(ulong hash)
        {
            hash ^= hash >> 16;
            hash *= 0x85ebca6b;

            hash ^= hash >> 13;
            hash *= 0xc2b2ae35;

            hash ^= hash >> 16;

            return hash;
        }

        // This is the part where multiply and rotate (MUR-MUR) happens
        private void MixBody(ulong k1, ulong k2, ulong k3, ulong k4)
        {
            hash1 ^= MixKey1(k1);

            hash1 = hash1.RotateLeft32(19);
            hash1 += hash2;
            hash1 = hash1 * 5 + 0x561ccd1b;

            hash2 ^= MixKey2(k2);

            hash2 = hash2.RotateLeft32(17);
            hash2 += hash3;
            hash2 = hash2 * 5 + 0x0bcaa747;

            hash3 ^= MixKey3(k3);

            hash3 = hash3.RotateLeft32(15);
            hash3 += hash4;
            hash3 = hash3 * 5 + 0x96cd1c35;

            hash4 ^= MixKey4(k4);

            hash4 = hash4.RotateLeft32(13);
            hash4 += hash1;
            hash4 = hash4 * 5 + 0x32ac3b17;
        }

        private static ulong MixKey1(ulong k1)
        {
            k1 *= C1;
            k1 = k1.RotateLeft32(15);
            k1 *= C2;
            return k1;

        }

        private static ulong MixKey2(ulong k2)
        {
            k2 *= C2;
            k2 = k2.RotateLeft32(16);
            k2 *= C3;
            return k2;
        }

        private static ulong MixKey3(ulong k3)
        {
            k3 *= C3;
            k3 = k3.RotateLeft32(17);
            k3 *= C4;
            return k3;
        }

        private static ulong MixKey4(ulong k4)
        {
            k4 *= C4;
            k4 = k4.RotateLeft32(18);
            k4 *= C1;
            return k4;
        }

    }
}
