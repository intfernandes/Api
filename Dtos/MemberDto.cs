


using Api.Entities;

namespace Api.Dtos
{
    public class MemberDto : UserDto
    {   
        public Guid? DomainId { get; set; }= null!; 
        public virtual Domain? Domain { get; set; } = null!; 
    }
}