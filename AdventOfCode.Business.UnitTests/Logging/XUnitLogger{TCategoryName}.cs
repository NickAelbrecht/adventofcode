using System;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace AdventOfCode.Business.UnitTests.Logging
{
    public class XUnitLogger<TCategoryName> : XUnitLogger, ILogger<TCategoryName>
    {
        public XUnitLogger(ITestOutputHelper output)
            : base(output, typeof(TCategoryName).FullName)
        {
        }

        public XUnitLogger(Func<ITestOutputHelper> outputFunc)
            : base(outputFunc, typeof(TCategoryName).FullName)
        {
        }
    }
}
