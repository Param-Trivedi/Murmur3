using System;
using System.Text;

namespace Murmur3_128bit
{
    class Program
    {
        public static string ByteArrayToString(byte[] finHash)
        {
            StringBuilder hex = new StringBuilder(finHash.Length * 2);
            foreach (byte temphash in finHash)
                hex.AppendFormat("{0:x2} ", temphash);
            return hex.ToString();
        }

        private static void Main(string[] args)
        {
            //128-bit x64 Architecture
            Murmur3_x64_128 mHash64 = new Murmur3_x64_128(0);
            string strHash64 = "Hello World";
            byte[] finHash64 = mHash64.ComputeHash(Encoding.ASCII.GetBytes(strHash64));

            //128-bit x86 Architecture
            Murmur3_x86_128 mHash86 = new Murmur3_x86_128(0);
            string strHash86 = "Hello World";
            byte[] finHash86 = mHash86.ComputeHash(Encoding.ASCII.GetBytes(strHash86));

            //Covert bytes to Hex
            string finHexHash64 = ByteArrayToString(finHash64);
            string finHexHash86 = ByteArrayToString(finHash86);

            //Output the Result on the screen
            Console.WriteLine("The Murmur3 Hash of 128-bit for string: \n' " + strHash64 + " '\nFor x64 Architecture : " + finHexHash64 + "\nFor x86 Architecture : x86 Architectur needed");


        }
    }
}
