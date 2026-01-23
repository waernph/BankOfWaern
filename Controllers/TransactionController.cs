using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data;


namespace Bank_of_Waern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private readonly IJwtHelper _jwtHelper;
        private readonly BankAppDataContext _context;

        public TransactionController(ITransactionService transactionService,
            IAccountService accountService, IJwtHelper jwtHelper, BankAppDataContext context)
        {
            _transactionService = transactionService;
            _accountService = accountService;
            _jwtHelper = jwtHelper;
            _context = context;
        }

        [Authorize(Roles = "User"), HttpGet("Transactions")]
        public async Task<IActionResult> Transactions(int accountId)
        {
            try
            {
                var customerId = await _jwtHelper.GetLoggedInCustomerId();
                var transactions = await _transactionService.GetAllTransactions(accountId, customerId);
                return Ok(transactions.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "User"), HttpPost("NewTransaction")]
        public async Task<IActionResult> NewTransaction(int recieverAccountId, int senderAccountId, decimal amount, string message)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _accountService.checkAccount(senderAccountId, amount);
                await _transactionService.NewTransaction(senderAccountId, $"Credit", message, amount);
                await _accountService.UpdateBalance(senderAccountId, -amount);
                await _transactionService.NewTransaction(recieverAccountId, $"Debit", message, amount);
                await _accountService.UpdateBalance(recieverAccountId, amount);
                await dbTransaction.CommitAsync();
                return StatusCode(201, "Transaction successful!");
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
        }



    }
}

