using Bank_of_Waern.Data.DTOs;
using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Core.Interfaces
{
    public interface IAccountService
    {
        public Task<Account> CreateAccount(string frequency, decimal balance, int accountTypeId, string? accountTypeDescription);
        public Task<List<AccountDTO>> GetAllAccounts(int customerId, List<Disposition> dispositions);
    }
}
