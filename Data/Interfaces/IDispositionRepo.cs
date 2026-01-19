using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface IDispositionRepo
    {
        public Task AddAccountToDisposition(int customerId, int dispositionId, int accountId, string type);
        Task<List<Disposition>> GetDisposition(int customerId);
        public Task<Disposition> SetupDisposition(int customerId, int accountId, string dispositionType);
    }
}
