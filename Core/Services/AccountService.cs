using AutoMapper;
using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.DTOs;
using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using Bank_of_Waern.Core.Interfaces;

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

        public async Task<Account> CreateAccount(string frequency, decimal balance, int accountTypeId, string? accountTypeDescription)
        {
            return await _accountRepo.CreateAccount(frequency, balance, accountTypeId, accountTypeDescription);
        }

        public async Task<List<AccountDTO>> GetAllAccounts(int customerId, Disposition disposition)
        {
            var accounts = await _accountRepo.GetAllAccounts(customerId, disposition);
            var mappedAccounts = _mapper.Map<List<AccountDTO>>(accounts);
            return mappedAccounts;
        }
    }
}
