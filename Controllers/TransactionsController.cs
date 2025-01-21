
using Api.Entities;
using Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{

    public class TransactionsController(DataContext context) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions() {
            var transactions = await context.Transactions.ToListAsync();

            return Ok(transactions);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int Id) {
            var transaction = await context.Transactions.FindAsync(Id);

            if(transaction == null) {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction(Transaction transaction) {
            if(transaction.CustomerId == 0) return BadRequest("Customer Id is required");
            if(transaction.ProductId == 0) return BadRequest("Product Id is required");
            if(transaction.Quantity <= 0) return BadRequest("Quantity must be greater than 0");
            if(transaction.Price <= 0) return BadRequest("Price must be greater than 0");
            
            var customerId = transaction.CustomerId;
            var customer = await context.Customers.FindAsync(customerId);
            var productId = transaction.ProductId;
            var product = await context.Products.FindAsync(productId);

            if(customer == null) return NotFound("Customer not found");
            if (product == null) return BadRequest("Product not found");

            context.Transactions.Add(transaction);

            await context.SaveChangesAsync();

            return Ok(transaction);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Transaction>> UpdateTransaction(int Id, Transaction transaction) {
            var existingTransaction = await context.Transactions.FindAsync(Id);
            if (existingTransaction == null) return NotFound();
            if (transaction.Quantity > 0) existingTransaction.Quantity = transaction.Quantity;
            if (transaction.CustomerId == 0) existingTransaction.CustomerId = transaction.CustomerId;
            if (transaction.Price > 0) existingTransaction.Price = transaction.Price;
            if (transaction.ProductId == 0) existingTransaction.ProductId = transaction.ProductId;
            
            await context.SaveChangesAsync();

            return Ok(existingTransaction); 
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Transaction>> DeleteTransaction(int Id) {
        var transaction = context.Transactions.Find();
        if (transaction == null) return NotFound();
         context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();
        return Ok(transaction);
    }
}   }
