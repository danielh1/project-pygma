using AutoFixture;
using Pygma.UatTests.Factories;
using Xunit;

namespace Pygma.UatTests.Base
{
    [Collection("Pygma.UatTests")]
    public class UatBase
    {
        public Fixture Fixture;
        
        public UatBase()
        {
            Fixture = FixtureFactory.CreateOmitOnRecursionFixture();
        }
    }
}