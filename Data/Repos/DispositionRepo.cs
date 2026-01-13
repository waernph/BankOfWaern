using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Data.Repos
{
    public class DispositionRepo : IDispositionRepo
    {
        private readonly BankAppDataContext _context;
        private readonly IAccountRepo _accountContext;

        public DispositionRepo(BankAppDataContext context, IAccountRepo accountContext)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public async Task<Disposition> SetupDisposition(int customerId, int accountId, string type)
        {
            var newDisposition = new Disposition
            {
                AccountId = accountId,
                CustomerId = customerId,
                Type = type
            };
            _context.Dispositions.Add(newDisposition);
            await _context.SaveChangesAsync();
            return newDisposition;
        }
    }
}
