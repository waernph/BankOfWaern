using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank_of_Waern.Data.Repos
{
    public class AccountRepo : IAccountRepo
    {
        private readonly BankAppDataContext _context;

        public AccountRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAccount(string frequency, decimal balance, int accountTypeId, string? accountTypeDescription)
        {
            var newAccount = new Account
            {
                Frequency = frequency,
                Created = DateOnly.FromDateTime(DateTime.Now),
                Balance = balance,
                AccountTypesId = accountTypeId
            };
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();
            return newAccount;

        }

        public async Task<List<Account>> GetAllAccounts(int customerId, Disposition disposition)
        {
            List<Account> accounts = await _context.Accounts
                                            .Where(a => a.AccountId == disposition.AccountId)
                                            .ToListAsync();
            
            return accounts;
        }
    }
}
