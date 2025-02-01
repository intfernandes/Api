


namespace Api.Dtos
{
    public class MemberDto : UserDto
    {   
        public  Guid Id {get; set;} = new Guid();
        public int CompanyId { get; set; } 
    }
}