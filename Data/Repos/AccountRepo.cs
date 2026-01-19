using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<Account> GetAccount(int accountId, Disposition dispostion)
        {
            var account = await _context.Accounts
                                  .FirstOrDefaultAsync(a => a.AccountId == accountId);
            if (account == null)
                throw new Exception("Account not found");
            else
                return account;
        }

        public async Task<List<Account>> GetAllAccounts(int customerId, List<Disposition> dispositions)
        {
            var accountIds = dispositions.Select(d => d.AccountId).ToList();
            var accounts = await _context.Accounts.Where(a => accountIds.Contains(a.AccountId)).ToListAsync();
            return accounts;
        }

        public async Task<decimal> GetBalance(int accountId)
        {
            var account = await _context.Accounts
                                  .FirstOrDefaultAsync(a => a.AccountId == accountId);
            var balance = account.Balance;
            return balance;
        }
    }
}
