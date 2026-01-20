using Bank_of_Waern.Data.DTOs;

namespace Bank_of_Waern.Core.Interfaces
{
    public interface ITransactionService
    {

        public Task<List<TransactionDTO>> GetAllTransactions(int accountId, int customerId);
        public Task NewTransaction(int accountId, string type, string operation, decimal amount);
    }
}
