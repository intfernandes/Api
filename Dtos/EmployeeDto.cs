namespace Api.Dtos
{
    public class EmployeeDto : UserDto
    {   
        public Guid? StoreId { get; set; }= null!; 
        public virtual StoreDto? Store { get; set; } = null!; 
    }
}