using System;
using AutoMapper;
using Pygma.Users.Mapping.Auto.Profiles;
using Xunit;

namespace Pygma.App.UnitTests
{
    public class AutoMapperConfigTests
    {
        [Theory]
        [InlineData(typeof(UserProfile))]
        public void Profile_CheckConfigValid_ShouldNotThrow(Type t)
        {
            var prof = Activator.CreateInstance(t) as Profile;

            Assert.NotNull(prof);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(prof);
            });

            config.AssertConfigurationIsValid();
        }
    }
}
