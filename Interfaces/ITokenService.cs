
namespace Api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(dynamic user);
    }
}