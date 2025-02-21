namespace Api.Entities
{
    public class Member : IUser
    {
        public Guid? DomainId { get; set; } = null!; 
        public virtual Domain? Domain { get; set; } = null!; 
        public bool IsDomainResponsible { get; set; } 
    }
}