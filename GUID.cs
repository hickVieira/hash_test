public static class GUID
{
    public enum Type
    {
        Alphanumeric,
        Hexadecimal,
        Octal,
    }
    const string _alphanumerics = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    const string _hexadecimals = "0123456789ABCDEF";
    const string _octals = "01234567";

    public static string Random(uint length, Random random, Type type)
    {
        string symbols = null;
        switch (type)
        {
            case Type.Alphanumeric:
                symbols = _alphanumerics;
                break;
            case Type.Hexadecimal:
                symbols = _hexadecimals;
                break;
            case Type.Octal:
                symbols = _octals;
                break;
        }

        if (symbols == null) return string.Empty;

        bool isHex = type == Type.Hexadecimal;
        if (isHex)
            length += 2;

        char[] output = new char[length];
        if (isHex)
        {
            output[0] = '0';
            output[1] = 'x';
        }
        for (int i = isHex ? 2 : 0; i < length; i++)
        {
            int index = random.Next(_hexadecimals.Length);
            output[i] = _hexadecimals[index];
        }

        return new string(output);
    }
}