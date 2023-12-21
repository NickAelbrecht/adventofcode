using AutoFixture;
using AutoFixture.AutoMoq;

namespace AdventOfCode.Business.UnitTests
{
    public static class DefaultAutoFixtureHelper
    {
        public static IFixture GetDefaultFixture()
        {
            var fixture = new Fixture();

            // Add AutoMoq
            fixture.Customize(new AutoMoqCustomization());

            return fixture;
        }
    }
}