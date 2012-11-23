using System;
using System.Reactive.Linq;
using System.Text;

namespace Common.Email.Specifications.Fx
{
    internal static class StringGenerator
    {
        private const int _asciiLowerLimit = 33;
        private const int _asciiUpperLimit = 126;

        public static string RandomAscii(int length)
        {
            var builder = new StringBuilder();
            var stream = Observable.Create<char>(observer => new RandomCharacterGenerator(_asciiLowerLimit, _asciiUpperLimit, observer));

            while (builder.Length < length)
            {
                using (stream.Take(length - builder.Length).Subscribe(result => builder.Append(result))) { }
            }

            return builder.ToString();
        }
    }
}