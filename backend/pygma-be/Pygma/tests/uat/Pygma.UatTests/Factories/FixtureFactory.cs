using System.Linq;
using AutoFixture;

namespace Pygma.UatTests.Factories
{
    public static class FixtureFactory
    {
        //from https://github.com/AutoFixture/AutoFixture/issues/337
        public static Fixture CreateOmitOnRecursionFixture()
        { 
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());//recursionDepth

            return fixture;
        }
    }
}