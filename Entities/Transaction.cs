
namespace Api.Entities
{
    public class Transaction(int id, int userId, int customerId, int productId, decimal price, int quantity )
    {
        public int Id { get; set;} = id;
        public required int UserId { get; set;}  = userId;
        public required int CustomerId { get; set;} = customerId;
        public required int ProductId { get; set;} = productId;
        public  required decimal Price { get; set;}  = price;
        public  int Quantity { get; set;} = quantity;
        private decimal Total { get; } = quantity * price;
        public  double Fee { get; set;} = 0;
        public  string TransactionType { get; set;} = "purchase";
        public  string Status { get; set;} = "pending";
        public  string Currency { get; set;} = "BRL";
        public string? Description { get; set;}
        public DateTime DateTime { get; set;} = DateTime.Now;


        public override string ToString()
        {
            return $"Id: {Id}, UserId: {UserId}, CustomerId: {CustomerId}, TransactionType: {TransactionType}, Price: {Price}, Quantity: {Quantity}, Total: {Total} Description: {Description}, DateTime: {DateTime}, Status: {Status}, Currency: {Currency}, Fee: {Fee}";
        }

        public override bool Equals(object? obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Transaction transaction = (Transaction)obj;
            return Id == transaction.Id && UserId == transaction.UserId && CustomerId == transaction.CustomerId && TransactionType == transaction.TransactionType && Price == transaction.Price && Quantity == transaction.Quantity && Total == transaction.Total && Description == transaction.Description && DateTime == transaction.DateTime && Status == transaction.Status && Currency == transaction.Currency && Fee == transaction.Fee;
        }

        public override int GetHashCode()
        {
            int hash = HashCode.Combine(Id, UserId, CustomerId, TransactionType, Price, Quantity, Description);
            hash = HashCode.Combine(hash, Total, DateTime, Status, Currency, Fee);
            return hash;
        }

        
    }
}