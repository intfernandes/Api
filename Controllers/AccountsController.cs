using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class AccountsController( IAccountsRepository accounts, IMapper mapper)  : V1Controller
    {
        [HttpGet] // GET: api/v1/accounts
        public async Task<ActionResult<IEnumerable<AccountDto>>> Get() {
            var result = await accounts.Get(); 

            var accountsDtos = mapper.Map<IEnumerable<AccountDto>>(result);

            return Ok(accountsDtos);
        }

        [HttpGet("{Id:Guid}")] // GET: api/v1/accounts/{id}
        public async Task<ActionResult<AccountDto>> GetById(Guid Id) {
            var result = await accounts.GetById(Id);

            if(result == null) return NotFound();
            
            var accountDto = mapper.Map<AccountDto>(result);

            return Ok(accountDto);
        }

        [HttpGet("/search={input:alpha}")] // GET: api/v1/accounts/search={input}
        public async Task<ActionResult<AccountDto>> Search(string input) {
            var result = await accounts.Search(input);

            if(result == null) return NotFound();
            
            var accountDto = mapper.Map<AccountDto>(result);

            return Ok(accountDto);
        }

        [HttpPut("{id:Guid}")] // PUT: api/v1/accounts/{id}
        [ProducesResponseType(typeof(AccountDto), 200)]
        public async Task<ActionResult<AccountDto>> Update(AccountDto account) {
            var existingUser = await accounts.GetById(account.Id);

            if(existingUser == null) return NotFound();
            
            if(account?.AccountType != null) existingUser.AccountType = account.AccountType ?? AccountType.Customer;
            if(account?.AccountStatus != null) existingUser.AccountStatus = account.AccountStatus ?? AccountStatus.Pending ;
            if(account?.Permissions != null) existingUser.Permissions = account.Permissions;
       
            return Ok(existingUser);
        }

        [HttpPost] // POST: api/v1/accounts
        [ProducesResponseType(typeof(AccountDto), 201)]
        public async Task<ActionResult<AccountDto>> Create(AccountDto account) {
            var result = await accounts.Create(account);

            if(result == null) return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpDelete("{id:Guid}")] // DELETE: api/v1/accounts/{id}
        public async Task<ActionResult> Delete(Guid id) {
            var result = await accounts.Delete(id);

            if(result) return NoContent();

            return NotFound();
        }
    }
}