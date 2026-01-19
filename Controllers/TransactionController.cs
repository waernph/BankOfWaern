using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank_of_Waern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IJwtHelper _jwtHelper;

        public TransactionController(ITransactionService transactionService, IJwtHelper jwtHelper)
        {
            _transactionService = transactionService;
            _jwtHelper = jwtHelper;
        }

        [Authorize, HttpGet("Transactions")]
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
    }
}
