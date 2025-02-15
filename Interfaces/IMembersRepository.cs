
using Api.Dtos;
using Api.Entities.Users;

namespace Api.Interfaces
{
    public interface IMembersRepository
    {
        Task<MemberDto?> Create(SignUpDto user);
        Task<IEnumerable<MemberDto>> Get();
        Task<MemberDto?> GetById(Guid id);
        Task<MemberDto?> GetByName(string username);
        Task<MemberDto?> GetByEmail(string email);
        Task<MemberDto?> GetByPhoneNumber(string phoneNumber);
        Task<MemberDto?> Update(MemberDto user);
        Task<bool> Delete(Guid id);  
        Task<bool> Save();   
    }
}