namespace Api.Dtos
{
    public class EmployeeDto : UserDto
    {   
        public Guid? StoreId { get; set; }= null!; 
    }
}