using System;

namespace AdventOfCode.Business.UnitTests.Logging
{
    internal class NullScope : IDisposable
    {
        private NullScope()
        {
        }

        public static NullScope Instance { get; } = new NullScope();

        public void Dispose()
        {
        }
    }
}
