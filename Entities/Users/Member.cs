
namespace Api.Entities.Users
{
    public class Member : IUser
    {
           public int CompanyId { get; set; } 
           public virtual Company Company { get; set; } = null!; 

    }
}