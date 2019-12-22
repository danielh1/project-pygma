namespace Pygma.Users.Services
{
    public interface IJwtTokenService
    {
        string BuildToken(int userId);
    }
}