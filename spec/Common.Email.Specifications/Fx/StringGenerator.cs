using System;

namespace Common.Email.Specifications.Fx
{
    internal static class StringGenerator
    {
        private static readonly Random _random = new Random();

        private const int _asciiLowerLimit = 33;
        private const int _asciiUpperLimit = 126;

        public static string RandomAscii(int length)
        {
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = (char)_random.Next(_asciiLowerLimit, _asciiUpperLimit);
            }
            return new string(chars);
        }
    }
}