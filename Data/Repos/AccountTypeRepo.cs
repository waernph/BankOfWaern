using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Data.Repos
{
    public class AccountTypeRepo : IAccountTypeRepo
    {
        private readonly BankAppDataContext _context;
        public AccountTypeRepo(BankAppDataContext context)
        {
            _context = context;
        }
        public async Task<AccountType> CreateAccountType(string type, string ? description)
        {
            var newAccountType = new AccountType
            {
                TypeName = type,
                Description = description
            };
            _context.AccountTypes.Add(newAccountType);
            await _context.SaveChangesAsync();
            return newAccountType;
        }
    }
}
