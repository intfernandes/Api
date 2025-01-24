
using Api.Entities;

namespace Api.Dtos
{
    public class UserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Status { get; set; }
        public string? Gender { get; set;}
        
        public static UserDto FromUser(User user) {
            return new UserDto {
                Name = user.Name,
                Email = user.Email,
                Status = user.Status,
                Gender = user.Gender,
            };
        }
    }
}