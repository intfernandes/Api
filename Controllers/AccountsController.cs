
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
        public async Task<ActionResult<Account>> UpdateAccount( Account account) {
            var existingAccount = await context.Accounts.SingleOrDefaultAsync(x => x.Id == account.Id);

            if(existingAccount == null) return NotFound();
            
            if(account?.Instance != null) existingAccount.Instance = account.Instance;
            if(account?.Permissions != null ) existingAccount.Permissions = account.Permissions;
            if(account?.AccountType != null) existingAccount.AccountType = account.AccountType;
            if(account?.AccountStatus != null) existingAccount.AccountStatus = account.AccountStatus;

            await context.SaveChangesAsync();

            return Ok(existingAccount);
        }

        [HttpDelete("{id:int}")] 
        public async Task<ActionResult> DeleteAccount(Guid id) {
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