
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{

    public class AccountsController( DataContext context )  : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts() {
                     var accounts = await context.Accounts.ToListAsync();
    
                return Ok(accounts);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Account>> GetAccount(int Id) {
            var account = await context.Accounts.FindAsync(Id);

            if(account == null) {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPost] 
        public async Task<ActionResult> CreateAccount(Account account) {
            if (account == null) return BadRequest("Account is required");

            var existingAccount = await context.Accounts.SingleOrDefaultAsync(x => x.Id == account.Id);

             if (existingAccount != null) {

           return Ok(existingAccount);
             
             } else {
   context.Accounts.Add(account);
            await context.SaveChangesAsync();
            return Ok(account);
             }


         
        }

        [HttpPut("{id:int}")] 
        public async Task<ActionResult<Account>> UpdateAccount(int Id, Account account) {
            var existingAccount = await context.Accounts.FindAsync(Id);

            if(existingAccount == null) {
                return NotFound();
            }

            if(account.AccountName.Length > 0) existingAccount.AccountName = account.AccountName;
            if(account.AccountType.Length > 0) existingAccount.AccountType = account.AccountType;
            if(account.Balance > 0) existingAccount.Balance = account.Balance;
            
            await context.SaveChangesAsync();

            return Ok(existingAccount);
        }

        [HttpDelete("{id:int}")] 
        public async Task<ActionResult> DeleteAccount(int id) {
            var existingAccount = await context.Accounts.Where(x => x.Id == id ).ToListAsync();
            if(existingAccount == null) {
                return NotFound();
            }
            context.Accounts.RemoveRange(existingAccount);

            await context.SaveChangesAsync();
            return Ok();
        }
    }
}