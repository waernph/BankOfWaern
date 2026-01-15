using Bank_of_Waern.Core.Interfaces;
using BrewHub.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank_of_Waern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly IAccountTypeService _accountTypeService;
        private readonly IDispositionService _dispositionService;
        private readonly IJwtHelper _jwtHelper;
        private readonly IAdminService _adminService;

        public AdminController(ICustomerService customerService, IAccountService accountService, 
            IAccountTypeService accountTypeService, IDispositionService dispositionService, 
            IJwtHelper jwtHelper, IAdminService adminService)
        {
            _customerService = customerService;
            _accountService = accountService;
            _accountTypeService = accountTypeService;
            _dispositionService = dispositionService;
            _jwtHelper = jwtHelper;
            _adminService = adminService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AdminLogin(string email, string password)
        {
            try
            {
                var admin = await _adminService.AdminLogin(email, password);
                var token = await _jwtHelper.GetToken("Admin", admin.Email);
                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
        }
    }
}
