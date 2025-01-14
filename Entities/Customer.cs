
namespace Api.Entities
{
    public class Customer(int id, string name, string email)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Phone  { get; set; } = email; 
        public string? Address { get; set;}
        public string? State { get; set;}
        public string? City { get; set;}
        public string? ZipCode { get; set;}
        public string? CustomerGender { get; set;}
        public string? CustomerType { get; set;}
        public string? DateOfBirth { get; set;}
        public string? Photo { get; set;} 
        public DateTime? CreatedAt { get; set;} = DateTime.Now;
        public string? CreatedBy { get;} = "Admin";
        public int? AccountId{ get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Email: {Email}";
        }

        public override bool Equals(object? obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Customer customer = (Customer)obj;
            return Id == customer.Id && Name == customer.Name && Email == customer.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Email);
        }

        
    }
}