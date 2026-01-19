using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Data.Repos
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly BankAppDataContext _context;

        public TransactionRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetAllTransactions(int accountId)
        {
            var transactions = _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .ToList();
            return transactions;
        }

        public async Task NewTransaction(int accountId, DateOnly date, string type, string operation, decimal amount, decimal balance)
        {
            var newTransaction = new Transaction
            {
                AccountId = accountId,
                Date = date,
                Type = type,
                Operation = operation,
                Amount = amount,
                Balance = balance
            };
            _context.Transactions.Add(newTransaction);
            await _context.SaveChangesAsync();
        }
    }
}
