using Bank_of_Waern.Core.Interfaces;

using BrewHub.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;


namespace Bank_of_Waern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly IAccountTypeService _accountTypeService;
        private readonly IDispositionService _dispositionService;
        private readonly IJwtHelper _jwtHelper;

        public CustomerController(ICustomerService customerService,
            IAccountService accountService, IAccountTypeService accountTypeService,
            IDispositionService dispositionService, IJwtHelper jwtHelper)
        {
            _customerService = customerService;
            _accountService = accountService;
            _accountTypeService = accountTypeService;
            _dispositionService = dispositionService;
            _jwtHelper = jwtHelper;
        }

        [Authorize]
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            try
            {
                await _customerService.ChangePassword(oldPassword, newPassword, confirmPassword);
                return Ok("Password changed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string birthday, string email, string password)
        {
            try
            {
                var user = await _customerService.Login(birthday, email, password);
                var token = await _jwtHelper.GetToken("User", email, user);
                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize, HttpGet("Accounts")]
        public async Task<IActionResult> Accounts()
        {
            try
            {
                var customerId = await _jwtHelper.GetLoggedInCustomerId();
                var disposition = await _dispositionService.GetDisposition(customerId);
                var accounts = await _accountService.GetAllAccounts(customerId, disposition);
                return Ok(accounts.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }
        [Authorize, HttpGet("Transactions")]
        public async Task<IActionResult> Transactions(int accountId)
        {
            try
            {
                var customerId = await _jwtHelper.GetLoggedInCustomerId();
                var transactions = await _accountService.GetAllTransactions(accountId);
                return Ok(transactions.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
