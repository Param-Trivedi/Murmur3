# Murmur3
Murmur3 Hashing Algorithm
This library is based on the Implementation of Murmur3[a link](https://github.com/aappleby/smhasher/wiki/MurmurHash3) Hashing alogrithm created by Austin Appleby 

At this point the library supports 128-bit x64 Architecture format.
Please read CHANGELOG.md to follow up on the latest releases.

# Example for 128-bit x64 Architecture

```
	    Murmur3_x64_128 mHash = new Murmur3_x64_128(0);
            string strHash = "Hello World";
            byte[] finHash = mHash.ComputeHash(Encoding.ASCII.GetBytes(strHash));
```
