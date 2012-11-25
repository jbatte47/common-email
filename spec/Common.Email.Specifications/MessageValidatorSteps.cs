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
            string randomAscii = StringGenerator.RandomAscii(characterCount);
            ScenarioContext.Current[LineUnderTest] = randomAscii;
            var validator = ScenarioContext.Current[Keys.MessageValidator] as IMessageValidator;
            Debug.Assert(validator != null);
            try
            {
                validator.ValidateLine(randomAscii);
            }
            catch (Exception error)
            {
                Debug.WriteLine(error);
                ScenarioContext.Current[Keys.ExceptionDuringCall] = error;
            }
        }

        [When(@"I validate (.*)"), Scope(Tag = Keys.StaticInputTag)]
        public void ValidateInputString(string input)
        {
            ScenarioContext.Current[LineUnderTest] = input;
            var validator = ScenarioContext.Current[Keys.MessageValidator] as IMessageValidator;
            Debug.Assert(validator != null);
            try
            {
                validator.ValidateString(input);
            }
            catch (Exception error)
            {
                Debug.WriteLine(error);
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

        [Then(@"no calls should be made to the logger")]
        public void ExpectNoLoggerCalls()
        {
            ExpectNoTraceCalls();
            ExpectNoTraceFormatCalls();
            ExpectNoWarnCalls();
            ExpectNoWarnFormatCalls();
            ExpectNoDebugCalls();
            ExpectNoDebugFormatCalls();
            ExpectNoErrorCalls();
            ExpectNoErrorFormatCalls();
            ExpectNoFatalCalls();
            ExpectNoFatalFormatCalls();
        }

        private void ExpectNoTraceCalls()
        {
            _mockLogger.Verify(log => log.Trace(It.IsAny<Action<FormatMessageHandler>>()), Times.Never());
            _mockLogger.Verify(log => log.Trace(It.IsAny<Action<FormatMessageHandler>>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.Trace(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>()), Times.Never());
            _mockLogger.Verify(log => log.Trace(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.Trace(It.IsAny<object>()), Times.Never());
            _mockLogger.Verify(log => log.Trace(It.IsAny<object>(), It.IsAny<Exception>()), Times.Never());
        }

        private void ExpectNoTraceFormatCalls()
        {
            _mockLogger.Verify(log => log.TraceFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.TraceFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.TraceFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>()), Times.Never());
            _mockLogger.Verify(log => log.TraceFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.TraceFormat(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.TraceFormat(It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.TraceFormat(It.IsAny<string>()), Times.Never());
            _mockLogger.Verify(log => log.TraceFormat(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never());
        }

        private void ExpectNoWarnCalls()
        {
            _mockLogger.Verify(log => log.Warn(It.IsAny<Action<FormatMessageHandler>>()), Times.Never());
            _mockLogger.Verify(log => log.Warn(It.IsAny<Action<FormatMessageHandler>>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.Warn(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>()), Times.Never());
            _mockLogger.Verify(log => log.Warn(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.Warn(It.IsAny<object>()), Times.Never());
            _mockLogger.Verify(log => log.Warn(It.IsAny<object>(), It.IsAny<Exception>()), Times.Never());
        }

        private void ExpectNoWarnFormatCalls()
        {
            _mockLogger.Verify(log => log.WarnFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.WarnFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.WarnFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>()), Times.Never());
            _mockLogger.Verify(log => log.WarnFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.WarnFormat(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.WarnFormat(It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.WarnFormat(It.IsAny<string>()), Times.Never());
            _mockLogger.Verify(log => log.WarnFormat(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never());
        }

        private void ExpectNoDebugCalls()
        {
            _mockLogger.Verify(log => log.Debug(It.IsAny<Action<FormatMessageHandler>>()), Times.Never());
            _mockLogger.Verify(log => log.Debug(It.IsAny<Action<FormatMessageHandler>>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.Debug(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>()), Times.Never());
            _mockLogger.Verify(log => log.Debug(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.Debug(It.IsAny<object>()), Times.Never());
            _mockLogger.Verify(log => log.Debug(It.IsAny<object>(), It.IsAny<Exception>()), Times.Never());
        }

        private void ExpectNoDebugFormatCalls()
        {
            _mockLogger.Verify(log => log.DebugFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.DebugFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.DebugFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>()), Times.Never());
            _mockLogger.Verify(log => log.DebugFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.DebugFormat(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.DebugFormat(It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.DebugFormat(It.IsAny<string>()), Times.Never());
            _mockLogger.Verify(log => log.DebugFormat(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never());
        }

        private void ExpectNoErrorCalls()
        {
            _mockLogger.Verify(log => log.Error(It.IsAny<Action<FormatMessageHandler>>()), Times.Never());
            _mockLogger.Verify(log => log.Error(It.IsAny<Action<FormatMessageHandler>>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.Error(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>()), Times.Never());
            _mockLogger.Verify(log => log.Error(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.Error(It.IsAny<object>()), Times.Never());
            _mockLogger.Verify(log => log.Error(It.IsAny<object>(), It.IsAny<Exception>()), Times.Never());
        }

        private void ExpectNoErrorFormatCalls()
        {
            _mockLogger.Verify(log => log.ErrorFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.ErrorFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.ErrorFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>()), Times.Never());
            _mockLogger.Verify(log => log.ErrorFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.ErrorFormat(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.ErrorFormat(It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.ErrorFormat(It.IsAny<string>()), Times.Never());
            _mockLogger.Verify(log => log.ErrorFormat(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never());
        }

        private void ExpectNoFatalCalls()
        {
            _mockLogger.Verify(log => log.Fatal(It.IsAny<Action<FormatMessageHandler>>()), Times.Never());
            _mockLogger.Verify(log => log.Fatal(It.IsAny<Action<FormatMessageHandler>>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.Fatal(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>()), Times.Never());
            _mockLogger.Verify(log => log.Fatal(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.Fatal(It.IsAny<object>()), Times.Never());
            _mockLogger.Verify(log => log.Fatal(It.IsAny<object>(), It.IsAny<Exception>()), Times.Never());
        }

        private void ExpectNoFatalFormatCalls()
        {
            _mockLogger.Verify(log => log.FatalFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.FatalFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.FatalFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>()), Times.Never());
            _mockLogger.Verify(log => log.FatalFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.FatalFormat(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never());
            _mockLogger.Verify(log => log.FatalFormat(It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Never());
            _mockLogger.Verify(log => log.FatalFormat(It.IsAny<string>()), Times.Never());
            _mockLogger.Verify(log => log.FatalFormat(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never());
        }
    }
}