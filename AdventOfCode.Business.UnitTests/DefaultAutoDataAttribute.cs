using System;
using AutoFixture.Xunit2;

namespace AdventOfCode.Business.UnitTests
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DefaultAutoDataAttribute : AutoDataAttribute
    {
        public DefaultAutoDataAttribute()
            : base(DefaultAutoFixtureHelper.GetDefaultFixture)
        {
        }
    }
}
