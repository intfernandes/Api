using Api.Entities;
using Api.Entities.Users;

namespace Api.Entities.Users
{
    public class Member : IUser
    {
        // Foreign Key to Company - making it non-nullable to enforce that each Member belongs to one Company
        public Guid CompanyId { get; set; }
        public virtual Company? Company { get; set; } = null!; // One-to-many: Each Member belongs to one Company (Company has many Members)

        public bool IsCompanyResponsible { get; set; } // Property to designate if a Member is a 'Responsible' member for the Company
    }
}