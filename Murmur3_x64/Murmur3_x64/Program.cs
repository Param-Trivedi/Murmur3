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
            Murmur3_x64_128 mHash = new Murmur3_x64_128(0);
            string strHash = "Hello World";
            byte[] finHash = mHash.ComputeHash(Encoding.ASCII.GetBytes(strHash));

            //Covert bytes to Hex
            string finHexHash = ByteArrayToString(finHash);

            //Output the Result on the screen
            Console.WriteLine("The Murmur3 Hash of 128-bit x64 Architecture for string " + strHash + " is : \n" + finHexHash);


        }
    }
}
