using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank_of_Waern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtHelper _jwtHelper;
        private readonly IAccountService _accountService;
        private readonly IDispositionService _dispositionService;

        public AccountController(IJwtHelper jwtHelper, IAccountService accountService, 
            IDispositionService dispositionService)
        {
            _jwtHelper = jwtHelper;
            _accountService = accountService;
            _dispositionService = dispositionService;
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
    }
}
