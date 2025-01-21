namespace Api.Entities;

public class User
{
    public int Id { get; set;}
    public required string Name { get; set;}
    public required string Email { get; set;}
    public required byte[] PwdHash { get; set; }
    public required byte[] PwdSalt { get; set; }

}
