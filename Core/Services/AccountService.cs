using AutoMapper;
using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.DTOs;
using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;
        private readonly IJwtHelper _jwtHelper;
        private readonly IDispositionRepo _dispositionRepo;
        private readonly ITransactionRepo _transactionRepo;

        public AccountService(IAccountRepo accountRepo, IMapper mapper, IJwtHelper jwtHelper,
            IDispositionRepo dispositionRepo, ITransactionRepo transactionRepo)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
            _dispositionRepo = dispositionRepo;
            _transactionRepo = transactionRepo;
        }

        public async Task checkAccount(int accountId, decimal? amount)
        {
            var customerId = await _jwtHelper.GetLoggedInCustomerId();
            var dispositions = await _dispositionRepo.GetAllDispositions(customerId);
            if (!dispositions.Any(d => d.AccountId == accountId))
            {
                throw new Exception("Account does not belong to the logged in customer");
            }
            else
            {
                var account = await _accountRepo.GetSingleAccount(accountId);
                if (amount != null && account.Balance < amount)
                {
                    throw new Exception("Insufficient funds");
                }
            }
        }

        public async Task<Account> CreateAccount(string frequency, decimal balance, int accountTypeId)
        {
            return await _accountRepo.CreateAccount(frequency, balance, accountTypeId);
        }

        public async Task<List<AccountDTO>> GetAllAccounts(int customerId, List<Disposition> dispositions)
        {
            var accounts = await _accountRepo.GetAllAccounts(customerId, dispositions);
            var mappedAccounts = _mapper.Map<List<AccountDTO>>(accounts);
            return mappedAccounts;
        }

        public async Task UpdateBalance(int AccountId, decimal amount)
        {
            await _accountRepo.UpdateBalance(AccountId, amount);
        }
    }
}
