using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Core.Services
{
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IAccountTypeRepo _accountTypeRepo;

        public AccountTypeService(IAccountTypeRepo accountTypeRepo)
        {
            _accountTypeRepo = accountTypeRepo;
        }

        public async Task<AccountType> CreateAccountType(string type, string? description)
        {
            var newAccountType = await _accountTypeRepo.CreateAccountType(type, description);
            return newAccountType;
        }
    }
}
