using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface IAccountTypeRepo
    {
        public Task<AccountType> CreateAccountType(string type, string ? description);
    }
}
