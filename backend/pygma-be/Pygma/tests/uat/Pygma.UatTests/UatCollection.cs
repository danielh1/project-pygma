using Pygma.UatTests.Infrastructure;
using Xunit;

namespace Pygma.UatTests
{
    [CollectionDefinition("Pygma.UatTests")]
    public class UatCollection: ICollectionFixture<HttpTestClient>
    {
        
    }
}