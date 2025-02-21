namespace Api.Dtos
{
    public class MemberDto : UserDto
    {   
        public Guid? DomainId { get; set; }= null!; 
        public virtual DomainDto? Domain { get; set; } = null!; 
    }
}