using System;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace AdventOfCode.Business.UnitTests.Logging
{
    public class XUnitLoggerProvider : ILoggerProvider
    {
        private readonly Func<ITestOutputHelper> _output;

        public XUnitLoggerProvider(Func<ITestOutputHelper> output)
        {
            _output = output;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new XUnitLogger(_output, categoryName);
        }

        public void Dispose()
        {
        }
    }
}
