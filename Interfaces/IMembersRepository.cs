
using Api.Dtos;
using Api.Entities;


namespace Api.Interfaces
{
    public interface IMembersRepository
    {
        Task<Member?> Create(SignUpDto user);
        Task<Member?> SignIn(SignInDto signIn);
        Task<IEnumerable<Member>> Get();
        Task<Member?> GetById(Guid id);
        Task<Member?> GetByName(string username);
        Task<Member?> GetByEmail(string email);
        Task<Member?> GetByPhoneNumber(string phoneNumber);
        Task<Member?> Update(MemberDto user);
        Task<bool> Delete(Guid id);  
        Task<bool> Save();   
    }
}