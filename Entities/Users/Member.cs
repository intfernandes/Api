using Api.Entities;
using Api.Entities.Users;

namespace Api.Entities.Users
{
    public class Member : IUser
    {
        // Foreign Key to Domain - making it non-nullable to enforce that each Member belongs to one Domain
        public Guid DomainId { get; set; }
        public virtual Domain? Domain { get; set; } = null!; // One-to-many: Each Member belongs to one Domain (Domain has many Members)

        public bool IsDomainResponsible { get; set; } // Property to designate if a Member is a 'Responsible' member for the Domain
    }
}