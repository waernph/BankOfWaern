using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface IAccountRepo
    {
        public Task<Account> CreateAccount(string frequency, decimal balance, int accountTypeId, string ? accountTypeDescription);
        public Task<Account> GetAccount(int accountId, Disposition dispostion);
        public Task<List<Account>> GetAllAccounts(int customerId, Disposition disposition);
    }
}
