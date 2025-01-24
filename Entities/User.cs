namespace Api.Entities;

public class User
{
    public int Id { get; set;}
    public required string Email { get; set;}
    public required byte[] PwdHash { get; set; }
    public required byte[] PwdSalt { get; set; }
    public  string Name { get; set;} = string.Empty;
    public  string Status { get; set;}= string.Empty;
    public  string Gender { get; set;}= string.Empty;

}
