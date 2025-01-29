
namespace Api.Dtos
{
    public class RegisterResponseDto
    {
            public int Id { get; set;}
    public required string Name { get; set;} = string.Empty;
    public required string Email { get; set;}
    public required string Token { get; set;}
    }
}