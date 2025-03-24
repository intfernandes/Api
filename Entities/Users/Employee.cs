namespace Api.Entities
{
    public class Employee : IUser
    {
        public Guid StoreId { get; set; } 
        public virtual Store? Store { get; set; } 
        public bool IsStoreResponsible { get; set; } 
    }
}