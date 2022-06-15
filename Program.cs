namespace hash_test;

/*
best options: 
    fnv32
        +super fast 
        +compact 
        -minor collisions (~100 at 1M) (~1 at 100k)
    sha132
        +fast
        +compact
        -minor collisions (~100 at 1M) (~1 at 100k)
    sha164
        +fast
        -not compact (long ints)
        +no 

overall I think that I will use fnv32 hashing for my game's guid system - I obviously will never ever reach 100k+ indexed prefabs/assets
*/
// note: some of the input/output are limited to 32bit/small-sequences
class Program
{
    static void Main(string[] args)
    {
        Console.Write("random strings count = ");
        int count = int.Parse(Console.ReadLine());
        const int maxInputLength = 8;
        const int maxHashLength = 4;

        // guid
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string guid = System.Guid.NewGuid().ToString().Substring(0, maxInputLength);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(guid))
                    collisionCount++;
                else
                    hashes.Add(guid);
            }
            Console.WriteLine($"guid - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // guid_bytes
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string guid_bytes = System.Text.Encoding.ASCII.GetString(StringGUID.SystemGUIDBytes(maxInputLength));
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(guid_bytes))
                    collisionCount++;
                else
                    hashes.Add(guid_bytes);
            }
            Console.WriteLine($"guid_bytes - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // guid_random_random_seed
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string guid_random_random_seed = StringGUID.RandomAlphaNumeric(maxInputLength, i);
                timer.Stop();
                // Console.WriteLine(guid_random_random_seed);
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(guid_random_random_seed))
                    collisionCount++;
                else
                    hashes.Add(guid_random_random_seed);
            }
            Console.WriteLine($"guid_random_random_seed - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // guid_random_fixed_seed
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string guid_random_fixed_seed = StringGUID.RandomAlphaNumeric(maxInputLength, random);
                timer.Stop();
                // Console.WriteLine(guid_random_fixed_seed);
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(guid_random_fixed_seed))
                    collisionCount++;
                else
                    hashes.Add(guid_random_fixed_seed);
            }
            Console.WriteLine($"guid_random_fixed_seed - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // guid_random_crypto
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string guid_random_crypto = StringGUID.RandomAlphaNumeric(maxInputLength);
                timer.Stop();
                // Console.WriteLine(guid_random_crypto);
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(guid_random_crypto))
                    collisionCount++;
                else
                    hashes.Add(guid_random_crypto);
            }
            Console.WriteLine($"guid_random_crypto - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // guid_random_bytes0
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string guid_random_bytes0 = System.Text.Encoding.ASCII.GetString(StringGUID.RandomBytes(maxInputLength, i));
                timer.Stop();
                // Console.WriteLine(guid_random_bytes0);
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(guid_random_bytes0))
                    collisionCount++;
                else
                    hashes.Add(guid_random_bytes0);
            }
            Console.WriteLine($"guid_random_bytes0 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // guid_random_bytes1
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string guid_random_bytes1 = System.Text.Encoding.ASCII.GetString(StringGUID.RandomBytes(maxInputLength));
                timer.Stop();
                // Console.WriteLine(guid_random_bytes1);
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(guid_random_bytes1))
                    collisionCount++;
                else
                    hashes.Add(guid_random_bytes1);
            }
            Console.WriteLine($"guid_random_bytes1 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        return;

        // sha512_32
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString().Substring(0, maxInputLength);
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                byte[] hashBytes = Hashing.SHA512(inputBytes);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                Array.Resize(ref hashBytes, maxHashLength); string sha512_32 = Convert.ToHexString(hashBytes);

                if (hashes.Contains(sha512_32))
                    collisionCount++;
                else
                    hashes.Add(sha512_32);
            }
            Console.WriteLine($"sha512_32 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // sha256_32
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString().Substring(0, maxInputLength);
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                byte[] hashBytes = Hashing.SHA256(inputBytes);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                Array.Resize(ref hashBytes, maxHashLength); string sha256_32 = Convert.ToHexString(hashBytes);

                if (hashes.Contains(sha256_32))
                    collisionCount++;
                else
                    hashes.Add(sha256_32);
            }
            Console.WriteLine($"sha256_32 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // sha1_32
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString().Substring(0, maxInputLength);
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                byte[] hashBytes = Hashing.SHA1(inputBytes);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                Array.Resize(ref hashBytes, maxHashLength); string sha1_32 = Convert.ToHexString(hashBytes);

                if (hashes.Contains(sha1_32))
                    collisionCount++;
                else
                    hashes.Add(sha1_32);
            }
            Console.WriteLine($"sha1_32 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // md5_32
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString().Substring(0, maxInputLength);
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                byte[] hashBytes = Hashing.MD5(inputBytes);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                Array.Resize(ref hashBytes, maxHashLength); string md5_32 = Convert.ToHexString(hashBytes);

                if (hashes.Contains(md5_32))
                    collisionCount++;
                else
                    hashes.Add(md5_32);
            }
            Console.WriteLine($"md5_32 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // fnv32
        {
            HashSet<uint> hashes = new HashSet<uint>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString().Substring(0, maxInputLength);
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                uint fnv32 = Hashing.FNV32(inputBytes);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(fnv32))
                    collisionCount++;
                else
                    hashes.Add(fnv32);
            }
            Console.WriteLine($"fnv32 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // crc32
        {
            HashSet<uint> hashes = new HashSet<uint>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString().Substring(0, maxInputLength);
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                uint crc32 = Hashing.CRC32(inputBytes);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(crc32))
                    collisionCount++;
                else
                    hashes.Add(crc32);
            }
            Console.WriteLine($"crc32 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // fletcher64
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString().Substring(0, maxInputLength);
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string fletcher64 = Convert.ToHexString(Hashing.Fletcher(inputBytes, 64));
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(fletcher64))
                    collisionCount++;
                else
                    hashes.Add(fletcher64);
            }
            Console.WriteLine($"fletcher64 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // fletcher32
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString().Substring(0, maxInputLength);
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string fletcher32 = Convert.ToHexString(Hashing.Fletcher(inputBytes, 32));
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(fletcher32))
                    collisionCount++;
                else
                    hashes.Add(fletcher32);
            }
            Console.WriteLine($"fletcher32 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // fletcher16
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString().Substring(0, maxInputLength);
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string fletcher16 = Convert.ToHexString(Hashing.Fletcher(inputBytes, 16));
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(fletcher16))
                    collisionCount++;
                else
                    hashes.Add(fletcher16);
            }
            Console.WriteLine($"fletcher16 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // adler32
        {
            HashSet<uint> hashes = new HashSet<uint>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString().Substring(0, maxInputLength);
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var timer = System.Diagnostics.Stopwatch.StartNew();
                uint adler32 = Hashing.Adler32(inputBytes);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(adler32))
                    collisionCount++;
                else
                    hashes.Add(adler32);
            }
            Console.WriteLine($"adler32 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }
    }
}