using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface ITransactionRepo
    {
        public Task<List<Transaction>> GetAllTransactions(int accountId);
        public Task NewTransaction(int accountId, DateOnly date, string type, string operation, decimal amount, decimal balance);
    }
}
