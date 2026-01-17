using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;

        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<Account> CreateAccount(string frequency, decimal balance, int accountTypeId, string? accountTypeDescription)
        {
            return await _accountRepo.CreateAccount(frequency, balance, accountTypeId, accountTypeDescription);
        }

        public async Task<List<Account>> GetAllAccounts(int customerId, Disposition disposition)
        {
            var accounts = await _accountRepo.GetAllAccounts(customerId, disposition);
            return accounts;
        }
    }
}
