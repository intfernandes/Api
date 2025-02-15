
namespace Api.Entities
{
public enum Gender
{
    Unknown,
    Male,
    Female,
    Other
}



public enum AccountStatus 
{
    Active,
    Inactive,
    Pending, 
    Deleted
}

public enum AccountType
{
    Domain,
    Admin,
    Manager,
    Staff,
    Customer,
}

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Completed,
    Cancelled
}
}