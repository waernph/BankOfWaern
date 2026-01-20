using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface IDispositionRepo
    {
        public Task CreateDisposition(int customerId, int accountId, string type);
        Task<Disposition> GetDisposition(int customerId);
        Task<List<Disposition>> GetAllDispositions(int customerId);

        public Task<Disposition> SetupDisposition(int customerId, int accountId, string dispositionType);
    }
}
