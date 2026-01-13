using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Core.Services
{
    public class DispositionService : IDispositionService
    {
        private readonly IDispositionRepo _dispositionRepo;

        public DispositionService(IDispositionRepo dispositionRepo)
        {
            _dispositionRepo = dispositionRepo;
        }

        public async Task<Disposition> SetupDisposition(int customerId, int accountId, string dispositionType)
        {
            return await _dispositionRepo.SetupDisposition(customerId, accountId, dispositionType);

        }
    }
}
