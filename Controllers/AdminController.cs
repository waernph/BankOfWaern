using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly BankAppDataContext _context;
        private readonly IPasswordService _passwordService;

        public AdminController(ICustomerService customerService, IAccountService accountService, IAccountTypeService accountTypeService, 
            IDispositionService dispositionService, IJwtHelper jwtHelper, IAdminService adminService, BankAppDataContext context, IPasswordService passwordService)
        {
            _customerService = customerService;
            _accountService = accountService;
            _accountTypeService = accountTypeService;
            _dispositionService = dispositionService;
            _jwtHelper = jwtHelper;
            _adminService = adminService;
            _context = context;
            _passwordService = passwordService;
        }

        [AllowAnonymous, HttpGet("AdminLogin")]
        public async Task<IActionResult> AdminLogin(string email, string password)
        {

            try
            {
                var admin = await _adminService.AdminLogin(email, password);
                var token = await _jwtHelper.GetToken("Admin", admin.Email.ToString(), null);
                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
    }
}

