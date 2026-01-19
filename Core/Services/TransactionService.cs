using AutoMapper;
using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.DTOs;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Core.Services
{

    public class TransactionService : ITransactionService
    {
        private readonly IDispositionRepo _dispositionRepo;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IMapper _mapper;
        private readonly IAccountRepo _accountRepo;

        public TransactionService(IDispositionRepo dispositionRepo, ITransactionRepo transactionRepo, IMapper mapper, IAccountRepo accountRepo)
        {
            _dispositionRepo = dispositionRepo;
            _transactionRepo = transactionRepo;
            _mapper = mapper;
            _accountRepo = accountRepo;
        }

        public async Task<List<TransactionDTO>> GetAllTransactions(int accountId, int customerId)
        {
            var dispostion = await _dispositionRepo.GetAllDispositions(customerId);
            var accountIdsList = dispostion.Select(d => d.AccountId).ToList();
            if (accountIdsList.Contains(accountId)) // Fixa det här!!!! Ska checka av om accountId finns i listan av konton som tillhör customerId
            {
                var transactions = await _transactionRepo.GetAllTransactions(accountId);
                var mappedTransactions = _mapper.Map<List<TransactionDTO>>(transactions);
                return mappedTransactions;
            }
            else
            {
                throw new UnauthorizedAccessException("You do not have access to this account's transactions.");
            }
        }

        public async Task NewTransaction(int accountId, string type, string operation, decimal amount)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            decimal balance = await _accountRepo.GetBalance(accountId);
            await _transactionRepo.NewTransaction(accountId, date, type, operation, amount, balance);
        }
    }

}