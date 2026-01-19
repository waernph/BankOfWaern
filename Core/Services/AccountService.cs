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

        public AccountService(IAccountRepo accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
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
