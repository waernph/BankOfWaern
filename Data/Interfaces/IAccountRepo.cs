using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface IAccountRepo
    {
        public Task<Account> CreateAccount(string frequency, decimal balance, int accountTypeId);
        public Task<Account> GetAccount(int accountId, Disposition dispostion);
        public Task<List<Account>> GetAllAccounts(int customerId, List<Disposition> dispositions);
        public Task<decimal> GetBalance(int accountId);
        public Task<Account> GetSingleAccount(int accountId);
        public Task UpdateBalance(int accountId, decimal amount);
    }
}
