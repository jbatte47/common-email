namespace Common.Email
{
    /// <summary>
    /// Defines a message validator.
    /// </summary>
    public interface IMessageValidator
    {
        /// <summary>
        /// Validates the line.
        /// </summary>
        /// <param name="line">The line.</param>
        void ValidateLine(string line);
    }
}