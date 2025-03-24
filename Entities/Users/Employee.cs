namespace Api.Entities
{
    public class Employee : IUser
    {
        public Guid? StoreId { get; set; } = null!; 
        public virtual Store? Store { get; set; } = null!; 
        public bool IsStoreResponsible { get; set; } 
    }
}