using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank_of_Waern.Data.Repos
{
    public class LoanRepo : ILoanRepo
    {
        private readonly BankAppDataContext _context;
        public LoanRepo(BankAppDataContext context)
        {
            _context = context;
        }
        public async Task<Loan> ApplyForLoad(decimal amount, int duration, int accountId, int customerId, decimal payments)
        {
            var account = _context.Accounts.Where(a => a.AccountId == accountId).FirstOrDefault();
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
