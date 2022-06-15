public static class Hashing
{
    private class Crc32
    {
        private readonly uint _generator = 0xEDB88320;
        private static uint[] _checksumTable;

        public Crc32()
        {
            // Constructs the checksum lookup table. Used to optimize the checksum.
            if (_checksumTable == null || _checksumTable.Length == 0)
                _checksumTable = Enumerable.Range(0, 256).Select(i =>
                {
                    var tableEntry = (uint)i;
                    for (var j = 0; j < 8; ++j)
                    {
                        tableEntry = ((tableEntry & 1) != 0)
                            ? (_generator ^ (tableEntry >> 1))
                            : (tableEntry >> 1);
                    }
                    return tableEntry;
                }).ToArray();
        }

        public uint ComputeHash<T>(IEnumerable<T> byteStream)
        {
            // Initialize checksumRegister to 0xFFFFFFFF and calculate the checksum.
            return ~byteStream.Aggregate(0xFFFFFFFF, (checksumRegister, currentByte) =>
                      (_checksumTable[(checksumRegister & 0xFF) ^ Convert.ToByte(currentByte)] ^ (checksumRegister >> 8)));
        }
    }

    private static System.Security.Cryptography.SHA512 _sha512 = System.Security.Cryptography.SHA512.Create();
    private static System.Security.Cryptography.SHA256 _sha256 = System.Security.Cryptography.SHA256.Create();
    private static System.Security.Cryptography.SHA1 _sha1 = System.Security.Cryptography.SHA1.Create();
    private static System.Security.Cryptography.MD5 _md5 = System.Security.Cryptography.MD5.Create();
    private static Crc32 _crc32 = new Crc32();

    public static byte[] SHA512(byte[] inputBytes)
    {
        return _sha512.ComputeHash(inputBytes);
    }

    public static byte[] SHA256(byte[] inputBytes)
    {
        return _sha256.ComputeHash(inputBytes);
    }

    public static byte[] SHA1(byte[] inputBytes)
    {
        return _sha1.ComputeHash(inputBytes);
    }

    public static byte[] MD5(byte[] inputBytes)
    {
        return _md5.ComputeHash(inputBytes);
    }

    public static uint CRC32(byte[] inputBytes)
    {
        return _crc32.ComputeHash(inputBytes);
    }

    public static uint FNV32(byte[] inputBytes)
    {
        const uint fnv_offset_basis = 0x811c9dc5;
        const uint fnv_prime = 0x01000193;
        uint hash = fnv_offset_basis;

        for (uint i = 0; i < inputBytes.Length; i++)
        {
            hash ^= inputBytes[i];
            hash *= fnv_prime;
        }

        return hash;
    }

    public static uint Adler32(byte[] inputBytes)
    {
        const uint mod = 65521;
        uint a = 1;
        uint b = 0;
        for (int i = 0; i < inputBytes.Length; i++)
        {
            uint c = inputBytes[i];
            a = (a + c) % mod;
            b = (b + a) % mod;
        }
        return (b << 16) | a;
    }

    public static byte[] Fletcher(byte[] inputBytes, int n = 32) // Fletcher 32, 16, 64
    {
        IEnumerable<ulong> Blockify(IReadOnlyList<byte> inputAsBytes, int blockSize)
        {
            var i = 0;
            ulong block = 0;

            while (i < inputAsBytes.Count)
            {
                block = (block << 8) | inputAsBytes[i];
                i++;

                if (i % blockSize != 0 && i != inputAsBytes.Count) continue;

                yield return block;
                block = 0;
            }
        }

        var bytesPerCycle = n / 16;
        var modValue = (ulong)(Math.Pow(2, 8 * bytesPerCycle) - 1);

        ulong sum1 = 0;
        ulong sum2 = 0;

        foreach (var block in Blockify(inputBytes, bytesPerCycle))
        {
            sum1 = (sum1 + block) % modValue;
            sum2 = (sum2 + sum1) % modValue;
        }

        return BitConverter.GetBytes(sum1 + sum2 * (modValue + 1));
    }
}