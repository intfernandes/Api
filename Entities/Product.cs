
namespace Api.Entities
{
    public class Product(int id, string name, decimal price)
    {
        public int Id { get; set; } = id;
        public required string Name { get; set; } = name;
        public required decimal Price { get; set; } = price; 
        public string? Description { get; set;}

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Price: {Price}";
        }

        public override bool Equals(object? obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Product product = (Product)obj;
            return Id == product.Id && Name == product.Name && Price == product.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Price);
        }



    }
}