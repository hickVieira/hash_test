using System;
using System.Security;
using System.Security.Cryptography;

public static class StringGUID
{
    const string _alphanumerics = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static byte[] SystemGUIDBytes(uint length)
    {
        length = Math.Clamp(length, 1, 16);
        System.Guid guid = System.Guid.NewGuid();
        byte[] bytes = guid.ToByteArray();
        if (bytes.Length != length)
            Array.Resize(ref bytes, (int)length);
        return bytes;
    }

    public static string RandomAlphaNumeric(uint length, int seed)
    {
        Random random = new Random(seed);

        char[] output = new char[length];
        for (int i = 0; i < length; i++)
        {
            int index = random.Next(_alphanumerics.Length);
            output[i] = _alphanumerics[index];
        }
        return new string(output);
    }

    public static string RandomAlphaNumeric(uint length, Random random)
    {
        char[] output = new char[length];
        for (int i = 0; i < length; i++)
        {
            int index = random.Next(_alphanumerics.Length);
            output[i] = _alphanumerics[index];
        }
        return new string(output);
    }

    public static string RandomAlphaNumeric(uint length)
    {
        char[] output = new char[length];
        for (int i = 0; i < length; i++)
        {
            byte[] bytes = new byte[4];
            using (var crypto = RandomNumberGenerator.Create())
                crypto.GetBytes(bytes);
            uint rand = BitConverter.ToUInt32(bytes);
            int index = (int)rand % _alphanumerics.Length;
            if (index < 0) index = -index;
            output[i] = _alphanumerics[index];
        }
        return new string(output);
    }

    public static byte[] RandomBytes(uint length, int seed)
    {
        Random random = new Random(seed);
        byte[] bytes = new byte[length];
        random.NextBytes(bytes);
        return bytes;
    }

    public static byte[] RandomBytes(uint length)
    {
        byte[] bytes = new byte[length];
        using (var crypto = RandomNumberGenerator.Create())
            crypto.GetBytes(bytes);
        return bytes;
    }
}