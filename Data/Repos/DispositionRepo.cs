using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var disposition = await _context.Dispositions.Where(d => d.CustomerId == customerId).ToListAsync();
            return disposition;
        }

        public async Task<Disposition> GetDisposition(int customerId)
        {
            var disposition = await _context.Dispositions
                                  .FirstOrDefaultAsync(d => d.CustomerId == customerId);
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
