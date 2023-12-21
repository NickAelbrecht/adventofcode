using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace AdventOfCode.Business.UnitTests.Logging
{
    public class XUnitLogger : ILogger
    {
        private const string _loglevelPadding = ": ";
        private const string _messagePadding = "      ";
        private static readonly string _newLineWithMessagePadding = Environment.NewLine + _messagePadding;

        private readonly ITestOutputHelper _output;
        private readonly Func<ITestOutputHelper> _outputFunc;
        private readonly string _categoryName;

        public XUnitLogger(ITestOutputHelper output, string categoryName)
        {
            _output = output;
            _categoryName = categoryName;
        }

        public XUnitLogger(Func<ITestOutputHelper> outputFunc, string categoryName)
        {
            _outputFunc = outputFunc;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            // No scopes supported
            return NullScope.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // All logging enabled by default, except for none
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            // Do we need to log?
            if (_output == default)
            {
                return;
            }

            if (!IsEnabled(logLevel))
            {
                return;
            }

            // Build log
            var message = formatter(state, exception);

            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                // Write log
                WriteMessage(logLevel, _categoryName, eventId.Id, message, exception);
            }
        }

        private static string GetLogLevelString(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return "trce";
                case LogLevel.Debug:
                    return "dbug";
                case LogLevel.Information:
                    return "info";
                case LogLevel.Warning:
                    return "warn";
                case LogLevel.Error:
                    return "fail";
                case LogLevel.Critical:
                    return "crit";
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        private void WriteMessage(LogLevel logLevel, string logName, int eventId, string message, Exception exception)
        {
            // Example:
            // INFO: ConsoleApp.Program[10]
            //       Request received
            var logBuilder = new StringBuilder();

            // Log level
            var logLevelString = GetLogLevelString(logLevel);
            logBuilder.Append(logLevelString);

            // Category and event id
            logBuilder.Append(_loglevelPadding);
            logBuilder.Append(logName);
            logBuilder.Append("[");
            logBuilder.Append(eventId);
            logBuilder.AppendLine("]");

            if (!string.IsNullOrEmpty(message))
            {
                // Message
                logBuilder.Append(_messagePadding);

                var len = logBuilder.Length;
                logBuilder.AppendLine(message);
                logBuilder.Replace(Environment.NewLine, _newLineWithMessagePadding, len, message.Length);
            }

            // Example:
            // System.InvalidOperationException
            //    at Namespace.Class.Function() in File:line X
            if (exception != null)
            {
                // Exception message
                logBuilder.AppendLine(exception.ToString());
            }

            try
            {
                (_output ?? _outputFunc()).WriteLine(logBuilder.ToString());
            }
            catch (Exception)
            {
                // Ignore any exceptions thrown at this point
            }
        }
    }
}
