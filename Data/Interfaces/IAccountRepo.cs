using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface IAccountRepo
    {
        public Task<List<Account>> GetAllAccounts();
    }
}
