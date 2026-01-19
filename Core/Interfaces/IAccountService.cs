using Bank_of_Waern.Data.DTOs;
using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Core.Interfaces
{
    public interface IAccountService
    {
        public Task<Account> CreateAccount(string frequency, decimal balance, int accountTypeId, string? accountTypeDescription);
        Task<List<AccountDTO>> GetAllAccounts(int customerId, Disposition disposition);
    }
}
