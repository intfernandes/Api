
using Api.Dtos;
using Api.Entities;


namespace Api.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<Employee?> Create(SignUpDto dto);
        Task<Employee?> SignIn(SignInDto dto);
        Task<IEnumerable<Employee>> Get();
        Task<IEnumerable<Employee>?> Search(string input);
        Task<Employee?> GetById(Guid id);
        Task<Employee?> Update(EmployeeDto dto);
        Task<bool> Delete(Guid id);  
        Task<bool> Save();   
    }
}