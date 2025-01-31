
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
}

public enum AccountType
{
    Company,
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
    Cancelled
}
}