
namespace Api.Entities.Users
{
    public class Member : IUser
    {

           public Guid CompanyId { get; set; } 
           public virtual Company Company { get; set; } = null!; 
           

    }
}