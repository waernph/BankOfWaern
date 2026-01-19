using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface ITransactionRepo
    {
        public Task<List<Transaction>> GetAllTransactions(int accountId);
    }
}
