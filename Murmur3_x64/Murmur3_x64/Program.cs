using System;
using System.Text;

namespace Murmur3_x64
{
    class Program
    {
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2} ", b);
            return hex.ToString();
        }

        static void Main(string[] args)
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
