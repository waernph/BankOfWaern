using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Core.Interfaces
{
    public interface IDispositionService
    {
        public Task<Disposition> SetupDisposition(int customerId, int accountId, string dispositionType);
        public Task<Disposition> GetDisposition(int accountId);

    }
}
