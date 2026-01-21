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

        public AdminController(ICustomerService customerService, IAccountService accountService, 
            IAccountTypeService accountTypeService, IDispositionService dispositionService, 
            IJwtHelper jwtHelper, IAdminService adminService, BankAppDataContext context)
        {
            _customerService = customerService;
            _accountService = accountService;
            _accountTypeService = accountTypeService;
            _dispositionService = dispositionService;
            _jwtHelper = jwtHelper;
            _adminService = adminService;
            _context = context;
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
        [Authorize(Roles = "Admin"), HttpPost("NewCustomer")]
        public async Task<IActionResult> NewCustomer(string firstName, string lastName, string gender, string street,
            string city, string zip, string country, string countryCode, string birthday, string emailAdress,
            string phoneCountryCode, string phoneNumber, string frequency,
            decimal balance, int accountTypeId, string dispositionType)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newCustomer = await _customerService.CreateCustomer(firstName, lastName, gender, 
                    street, city, zip, country, countryCode, birthday, emailAdress, phoneCountryCode, phoneNumber);
                var tempPassword = newCustomer.Password;
                var newAccount = await _accountService.CreateAccount(frequency, balance, accountTypeId);
                var newDisposition = await _dispositionService.SetupDisposition(newCustomer.CustomerId, newAccount.AccountId, dispositionType);
                return StatusCode(201, $"New customer created! Temporary password: {newCustomer.Password} CHANGE WHEN YOU LOG IN.");
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                return BadRequest(ex.Message);
            }
        }
        
    }
}

