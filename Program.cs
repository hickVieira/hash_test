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

class Program
{
    static void Main(string[] args)
    {
        Console.Write("random strings count = ");
        int count = int.Parse(Console.ReadLine());

        // sha512
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString();
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                var timer = System.Diagnostics.Stopwatch.StartNew();
                byte[] hashBytes = Hashing.SHA512(input);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                Array.Resize(ref hashBytes, 4); string sha512 = Convert.ToHexString(hashBytes);

                if (hashes.Contains(sha512))
                    collisionCount++;
                else
                    hashes.Add(sha512);
            }
            Console.WriteLine($"sha512 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // sha256
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString();
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                var timer = System.Diagnostics.Stopwatch.StartNew();
                byte[] hashBytes = Hashing.SHA256(input);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                Array.Resize(ref hashBytes, 4); string sha256 = Convert.ToHexString(hashBytes);

                if (hashes.Contains(sha256))
                    collisionCount++;
                else
                    hashes.Add(sha256);
            }
            Console.WriteLine($"sha256 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // sha1
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString();
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                var timer = System.Diagnostics.Stopwatch.StartNew();
                byte[] hashBytes = Hashing.SHA1(input);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                Array.Resize(ref hashBytes, 4); string sha1 = Convert.ToHexString(hashBytes);

                if (hashes.Contains(sha1))
                    collisionCount++;
                else
                    hashes.Add(sha1);
            }
            Console.WriteLine($"sha1 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // md5
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString();
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                var timer = System.Diagnostics.Stopwatch.StartNew();
                byte[] hashBytes = Hashing.MD5(input);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                Array.Resize(ref hashBytes, 4); string md5 = Convert.ToHexString(hashBytes);

                if (hashes.Contains(md5))
                    collisionCount++;
                else
                    hashes.Add(md5);
            }
            Console.WriteLine($"md5 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // fnv
        {
            HashSet<uint> hashes = new HashSet<uint>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString();
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                var timer = System.Diagnostics.Stopwatch.StartNew();
                uint fnv = Hashing.FNV32(input);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(fnv))
                    collisionCount++;
                else
                    hashes.Add(fnv);
            }
            Console.WriteLine($"fnv32 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // CRC
        {
            HashSet<uint> hashes = new HashSet<uint>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString();
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                var timer = System.Diagnostics.Stopwatch.StartNew();
                uint crc = Hashing.CRC32(input);
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(crc))
                    collisionCount++;
                else
                    hashes.Add(crc);
            }
            Console.WriteLine($"crc32 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // Fletcher 64
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString();
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string fletcher = Convert.ToHexString(Hashing.Fletcher(input, 64));
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(fletcher))
                    collisionCount++;
                else
                    hashes.Add(fletcher);
            }
            Console.WriteLine($"fletcher64 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // Fletcher 32
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString();
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string fletcher = Convert.ToHexString(Hashing.Fletcher(input, 32));
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(fletcher))
                    collisionCount++;
                else
                    hashes.Add(fletcher);
            }
            Console.WriteLine($"fletcher32 - timeMS:{elapsedMS} collisions:{collisionCount}");
        }

        // Fletcher 16
        {
            HashSet<string> hashes = new HashSet<string>();
            long elapsedMS = 0;
            uint collisionCount = 0;
            for (int i = 0; i < count; i++)
            {
                string input = System.Guid.NewGuid().ToString();
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                var timer = System.Diagnostics.Stopwatch.StartNew();
                string fletcher = Convert.ToHexString(Hashing.Fletcher(input, 16));
                timer.Stop();
                elapsedMS += timer.ElapsedMilliseconds;

                if (hashes.Contains(fletcher))
                    collisionCount++;
                else
                    hashes.Add(fletcher);
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
                string input = System.Guid.NewGuid().ToString();
                // input = input.Substring(0, 16);
                // string input = i.ToString();
                var timer = System.Diagnostics.Stopwatch.StartNew();
                uint adler32 = Hashing.Adler32(input);
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