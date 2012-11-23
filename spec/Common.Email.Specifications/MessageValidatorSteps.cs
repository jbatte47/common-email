using System;
using System.Diagnostics;
using Common.Email.Specifications.Fx;
using Common.Logging;
using FluentAssertions;
using Moq;
using TechTalk.SpecFlow;

namespace Common.Email.Specifications
{
    [Binding]
    public class MessageValidatorSteps
    {
        public const string LineUnderTest = Keys.Base + "MessageValidator.LineUnderTest";
        private Mock<ILog> _mockLogger;

        [Given(@"I have an instance of the message validator")]
        public void GetMessageValidator()
        {
            _mockLogger = new Mock<ILog>();
            ScenarioContext.Current[Keys.MessageValidator] = new MessageValidator(_mockLogger.Object);
        }

        [When(@"I validate a line with (.*) characters")]
        public void ValidateRandomWithCount(int characterCount)
        {
            ScenarioContext.Current[LineUnderTest] = StringGenerator.RandomAscii(characterCount);
            var validator = ScenarioContext.Current[Keys.MessageValidator] as IMessageValidator;
            Debug.Assert(validator != null);
            try
            {
                validator.ValidateLine(ScenarioContext.Current[LineUnderTest] as string);
            }
            catch (Exception error)
            {
                ScenarioContext.Current[Keys.ExceptionDuringCall] = error;
            }
        }

        [Then(@"a format exception should be thrown during the validation call")]
        public void ExpectFormatException()
        {
            ScenarioContext.Current.Should().ContainKey(Keys.ExceptionDuringCall);

            ScenarioContext.Current[Keys.ExceptionDuringCall].Should()
                .NotBeNull().And.BeAssignableTo<FormatException>();
        }

        [Then(@"no exception should be thrown during the validation call")]
        public void ExpectNoErrors()
        {
            ScenarioContext.Current.Should().NotContainKey(Keys.ExceptionDuringCall);
        }

        [Then(@"the logger assigned to the validator should receive a warning")]
        public void ExpectLoggerWarning()
        {
            _mockLogger.Verify(log => log.WarnFormat(It.IsAny<string>(), It.IsAny<object[]>()));
        }
    }
}