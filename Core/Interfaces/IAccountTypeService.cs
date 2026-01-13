using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Core.Interfaces
{
    public interface IAccountTypeService
    {
        public Task<AccountType> CreateAccountType(string type, string? description);
    }
}
