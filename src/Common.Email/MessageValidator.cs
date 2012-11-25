using System;
using System.Linq;
using Common.Logging;

namespace Common.Email
{
    /// <summary>
    /// Validates Internet Message Format documents.
    /// See http://tools.ietf.org/html/rfc5322 for details.
    /// </summary>
    public class MessageValidator : IMessageValidator
    {
        private const int _suggestedLineMax = 78;
        private const int _requiredLineMax = 998;
        private readonly CharacterRange _generalRange = CharacterRange.Ascii;
        private readonly ILog _log;

        public MessageValidator(ILog log)
        {
            _log = log;
        }

        /// <summary>
        /// Validates the line according to the rules specified in RFC 5322; specifically the following sections:
        /// <list type="number">
        /// <item>2.1 (http://tools.ietf.org/html/rfc5322#section-2.1)</item>
        /// <item>2.1.1 (http://tools.ietf.org/html/rfc5322#section-2.1.1)</item>
        /// </list>
        /// </summary>
        /// <param name="line">The message line.</param>
        public void ValidateLine(string line)
        {
            ValidateString(line);
            var length = line.Length;
            if (length > _requiredLineMax) throw new FormatException(string.Format(MessageValidationResources.MaxLineLengthExceededFormat, length, _requiredLineMax));
            if (length > _suggestedLineMax) _log.WarnFormat(MessageValidationResources.RecommendedLineLengthExceededFormat, length, _suggestedLineMax);
        }

        public void ValidateString(string input)
        {
            var errors = input.Select((character, index) => new { Character = character, Index = index })
                .Where(item => !IsCharacterValid(item.Character, _generalRange))
                .Select(item => string.Format(MessageValidationResources.InvalidCharacterDetailFormat, item.Character, item.Index))
                .ToArray();

            if (errors.Length == 0) return;

            var helperBody = string.Join(MessageValidationResources.HelperSummarySeparator, errors);
            var helperSummary = string.Format(MessageValidationResources.InvalidStringHelperSummaryFormat, _generalRange.Name, helperBody);
            throw new FormatException(helperSummary);
        }

        private static bool IsCharacterValid(char character, CharacterRange range)
        {
            var value = (int) character;
            var badValue = value < range.Min || value > range.Max || range.SpecialExclusions.Any(bad => bad == value);
            var goodValue = range.SpecialInclusions.Any(good => good == value);
            return !badValue || goodValue;
        }
    }
}