using System;
using System.Collections.Generic;
using System.Text;
using Murmur3_x64;
using Xunit;


namespace Murmur3.Testing
{
    public class Murmur_128
    {
        

        [Fact(DisplayName = "Test_128(x64)")]
        public void Test_Murmur3_128_x64()
        {
            Murmur3_x64_128 mHash = new Murmur3_x64_128(0);
            string strHash = "Hello World";
            byte[] finHash = mHash.ComputeHash(Encoding.ASCII.GetBytes(strHash));

            //Covert bytes to Hex
            string finHexHash = ByteArrayToString(finHash);

            Assert.Equal("db c2 a0 c1 ab 26 63 1a 27 b4 c0 9f cf 1f e6 83 ", finHexHash);

        }

        private string ByteArrayToString(byte[] finHash)
        {
            StringBuilder hex = new StringBuilder(finHash.Length * 2);
            foreach (byte temphash in finHash)
                hex.AppendFormat("{0:x2} ", temphash);
            return hex.ToString();
        }
    }
}
