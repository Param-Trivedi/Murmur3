# Murmur3
Murmur3 Hashing Algorithm
This library is based on the Implementation of [Murmur3 Algorithm](https://github.com/aappleby/smhasher/wiki/MurmurHash3) Hashing alogrithm created by Austin Appleby. This project has taken references from other available implementation of the Algorithm.

At this point the library supports 128-bit x64 Architecture format.
Please read CHANGELOG.md to follow up on the latest releases.

# Example for 128-bit x64 Architecture

```
	    Murmur3_x64_128 mHash = new Murmur3_x64_128(0);
            string strHash = "Hello World";
            byte[] finHash = mHash.ComputeHash(Encoding.ASCII.GetBytes(strHash));
```
## Output
![output_128bit_x64](https://user-images.githubusercontent.com/10596504/42173045-ccb4998c-7deb-11e8-8c04-3d71e4e7d9f6.PNG)


# Future Notes
#### Support for 32-bit x64 and x86 architecture in develoment
#### Support for 128-bit x86 architecture in development
#### Avalance Test verification in development


