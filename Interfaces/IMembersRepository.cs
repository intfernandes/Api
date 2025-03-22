
using Api.Dtos;
using Api.Entities;


namespace Api.Interfaces
{
    public interface IMembersRepository
    {
        Task<Member?> Create(SignUpDto dto);
        Task<Member?> SignIn(SignInDto dto);
        Task<IEnumerable<Member>> Get();
        Task<IEnumerable<Member>?> Search(string input);
        Task<Member?> GetById(Guid id);
        Task<Member?> Update(MemberDto dto);
        Task<bool> Delete(Guid id);  
        Task<bool> Save();   
    }
}