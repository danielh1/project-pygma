using FluentAssertions;
using Pygma.UatTests.TestDb;
using Xunit;

namespace Pygma.UatTests.Tests
{
    public class DbSeedTests
    {
        [Fact]
        public void EnsureDbSeed()
        {
            var db = DbExtensions.CreateSqlDbContext();
            
            db.Users.Should().NotBeEmpty();
            db.BlogPosts.Should().NotBeEmpty();
            db.BlogPostComments.Should().NotBeEmpty();            
        }
    }
}