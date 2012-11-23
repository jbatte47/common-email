using System;

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
        private readonly ILog _log;

        public MessageValidator(ILog log)
        {
            _log = log;
        }

        /// <summary>
        /// Validates the line according to the rules specified in RFC 5322; specifically the following sections:
        /// <list type="number">
        /// <item>2.1.1 (http://tools.ietf.org/html/rfc5322#section-2.1.1)</item>
        /// </list>
        /// </summary>
        /// <param name="line">The message line.</param>
        public void ValidateLine(string line)
        {
            int length = line.Length;
            if (length > _requiredLineMax) throw new FormatException(string.Format(MessageValidationResources.MaxLineLengthExceededFormat, length, _requiredLineMax));
            if (length > _suggestedLineMax) _log.WarnFormat(MessageValidationResources.RecommendedLineLengthExceededFormat, length, _suggestedLineMax);
        }
    }
}