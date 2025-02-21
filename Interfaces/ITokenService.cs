
namespace Api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(dynamic user);
        string CreateRefreshToken(dynamic user);
    }
}