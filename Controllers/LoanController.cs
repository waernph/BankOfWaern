using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bank_of_Waern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly BankAppDataContext _context;
        private readonly ITransactionService _transactionService;

        public LoanController(ILoanService loanService, BankAppDataContext context, ITransactionService transactionService)
        {
            _loanService = loanService;
            _context = context;
            _transactionService = transactionService;
        }

        [Authorize(Roles = "Admin"), HttpPost("LoanApplication")]
        public async Task<IActionResult> LoanApplocation(int amount, int duration, int accountId, int customerId)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                string type = "Credit";
                string operation = "Loan";
                var grantedLoan = await _loanService.ApplyForLoan(amount, duration, accountId, customerId);
                await _transactionService.NewTransaction(accountId, type, operation, amount);
                dbTransaction.Commit();
                return Ok($"Loan granted and completed. Amount: {grantedLoan.LoanAmmount}. Payment: ${grantedLoan.payment}/month. " +
                    $"Loan is payed {DateOnly.FromDateTime(DateTime.Now).AddMonths(grantedLoan.Months)}");

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                return BadRequest(ex.Message);
            }

        }
    }
}
