using System;
using System.Collections.Generic;

namespace Common.Email
{
    public class CharacterRange
    {
        private const int _asciiMin = 1;
        private const int _asciiMax = 127;
        private const string _asciiName = "US-ASCII";
        private const string _headerFieldNameName = "Header Field Name (http://tools.ietf.org/html/rfc5322#section-2.2)";
        private const int _printableAsciiMin = 33;
        private const int _printableAsciiMax = 126;
        private static readonly Lazy<CharacterRange> _ascii = new Lazy<CharacterRange>(CreateAscii);
        private static readonly Lazy<CharacterRange> _headerFieldName = new Lazy<CharacterRange>(CreateHeaderFieldName);

        public CharacterRange()
        {
            SpecialExclusions = new int[]{};
            SpecialInclusions = new int[]{};
        }

        public string Name { get; private set; }
        public int Min { get; private set; }
        public int Max { get; private set; }
        public IEnumerable<int> SpecialExclusions { get; private set; }
        public IEnumerable<int> SpecialInclusions { get; private set; }

        public static CharacterRange Ascii
        {
            get { return _ascii.Value; }
        }

        public static CharacterRange HeaderFieldName
        {
            get { return _headerFieldName.Value; }
        }

        private static CharacterRange CreateAscii()
        {
            return new CharacterRange
            {
                Name = _asciiName,
                Min = _asciiMin,
                Max = _asciiMax
            };
        }

        private static CharacterRange CreateHeaderFieldName()
        {
            return new CharacterRange
            {
                Name = _headerFieldNameName,
                Min = _printableAsciiMin,
                Max = _printableAsciiMax,
                SpecialExclusions = new[] {58}
            };
        }
    }
}