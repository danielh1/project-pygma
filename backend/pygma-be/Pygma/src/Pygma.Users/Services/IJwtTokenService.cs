using Pygma.Data.Domain.Entities;

namespace Pygma.Users.Services
{
    public interface IJwtTokenService
    {
        string BuildToken(User user);
    }
}