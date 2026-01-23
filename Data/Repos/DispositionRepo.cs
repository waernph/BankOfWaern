using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Data.Repos
{
    public class DispositionRepo : IDispositionRepo
    {
        private readonly BankAppDataContext _context;

        public DispositionRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task CreateDisposition(int customerId, int accountId, string type)
        {
            var newDisposition = new Disposition
            {
                AccountId = accountId,
                CustomerId = customerId,
                Type = type
            };
            _context.Dispositions.Add(newDisposition);
            await _context.SaveChangesAsync();


        }

        public async Task<List<Disposition>> GetAllDispositions(int customerId)
        {
            var disposition = await _context.Dispositions.Where(d => d.CustomerId == customerId).AsNoTracking().ToListAsync();
            return disposition;
        }

        public async Task<Disposition> GetDisposition(int customerId)
        {
            var disposition = await _context.Dispositions
                                  .FirstOrDefaultAsync(d => d.CustomerId == customerId);
            if (disposition == null)
                throw new Exception("Disposition not found");
            else
                return disposition;
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
