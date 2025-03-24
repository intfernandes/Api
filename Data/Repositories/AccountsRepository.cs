using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class AccountsRepository(DataContext context) : IAccountsRepository
    {
        public async Task<Account?> Create(AccountDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            if (dto.AccountStatus == null || dto.AccountType == null) throw new ArgumentNullException();

Account account;

            if(dto.AccountType != AccountType.Customer) {
                 account = new Account
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = dto.UserId, 
                        AccountType = dto.AccountType ?? AccountType.Customer ,
                        AccountStatus = dto.AccountStatus ?? AccountStatus.Pending ,
                        Permissions = dto.Permissions,

                    };
                    await context.Accounts.AddAsync(account);
                    await Save();
                    return account;
            } 


            if (dto.StoreId == null) throw new ArgumentNullException("StoreId");
            
            account = new Account
                    {
                        Id = Guid.NewGuid(),
                        EmployeeId = dto.UserId,
                        StoreId = dto.StoreId,
                        AccountType = dto.AccountType ?? AccountType.Staff,
                        AccountStatus = dto.AccountStatus ?? AccountStatus.Pending ,
                        Permissions = dto.Permissions,

                    };
                    await context.Accounts.AddAsync(account);
                    await Save();
                    return account;
        }

        public async Task<IEnumerable<Account>> Get()
        {
            return await context.Accounts.ToListAsync();
        }

        public async Task<IEnumerable<Account>?> Search(string input)
        {

            return await context.Accounts
                .Where(x =>x.Permissions.Contains(input))
                .ToListAsync();
        }

        public async Task<Account?> GetById(Guid id)
        {
            return await context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account?> Update(AccountDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            if (dto.AccountStatus == null || dto.AccountType == null) throw new ArgumentNullException();
            
            var account = await GetById(dto.Id);

            if (account == null) return null;
            
            account.Permissions = dto.Permissions ?? account.Permissions;
            account.AccountStatus = dto.AccountStatus ?? account.AccountStatus; 
            account.AccountType = dto.AccountType ?? account.AccountType; 

            await Save();
            return account;
        }

        public async Task<bool> Delete(Guid id)
        {
            var account = await GetById(id);

            if (account == null) return false;

            context.Accounts.Remove(account);
            await Save();
            return true;
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        
    }}
}