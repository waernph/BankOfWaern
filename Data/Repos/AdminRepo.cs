using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using System.Diagnostics.Eventing.Reader;

namespace Bank_of_Waern.Data.Repos
{
    public class AdminRepo : IAdminRepo
    {
        private readonly BankAppDataContext _context;

        public AdminRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<Admin> AdminLogin(string email, string password)
        {
            var admin = _context.Admins.Where(a => a.Email == email).FirstOrDefault();
            if (admin == null)
                throw new Exception("Admin not found");

            else if (admin.Password != password)
                throw new Exception("Wrong email or password");
            else
                return admin;
            
        }

        public async Task<Loan> ApplyForLoad(decimal amount, int duration, int accountId, int customerId, decimal payments)
        {
            var account = _context.Accounts.Where(a =>a.AccountId == accountId).FirstOrDefault();
            var disposition = _context.Dispositions.Where(d => d.CustomerId == customerId).FirstOrDefault();

            if (account == null)
                throw new Exception("Not a registered customer.");
            else if (disposition.AccountId != accountId)
            {
                throw new Exception("Entered AccountId does not match AccountId connected to account");
            }
            else
            {
                var newLoan = new Loan
                {
                    AccountId = accountId,
                    Amount = amount,
                    Duration = duration,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Payments = payments,
                    Status = "Running"
                };
                account.Balance += amount;
                _context.Loans.Add(newLoan);
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
                return newLoan;
            }
        }
    }
}
