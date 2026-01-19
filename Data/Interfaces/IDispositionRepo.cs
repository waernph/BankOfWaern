using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface IDispositionRepo
    {
        Task<Disposition> GetDisposition(int customerId);
        public Task<Disposition> SetupDisposition(int customerId, int accountId, string dispositionType);
    }
}
