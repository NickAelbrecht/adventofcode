using System;
using AutoFixture.Xunit2;

namespace AdventOfCode.Business.UnitTests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DefaultInlineAutoDataAttribute : InlineAutoDataAttribute
    {
        public DefaultInlineAutoDataAttribute(params object[] values)
            : base(new DefaultAutoDataAttribute(), values)
        {
        }
    }
}
