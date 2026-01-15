using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Core.Services;
using Bank_of_Waern.Data.Entities;
using BrewHub.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

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

        [Authorize(Roles = "Admin")]
        [HttpPost("createCustomer")]
        public async Task<IActionResult> CreateCustomer(string firstName, string lastName, string gender, string street,
            string city, string zip, string country, string countryCode, string birthday, string emailAdress,
            string phoneCountryCode, string phoneNumber, string frequency,
            decimal balance, string accountType, string? accountTypeDescription, string dispositionType)
        {
            try
            {
                var newCustomer = await _customerService.CreateCustomer(firstName, lastName, gender, street, city, zip, country, countryCode, birthday, emailAdress, phoneCountryCode, phoneNumber);
                var accountTypeReturn = await _accountTypeService.CreateAccountType(accountType, accountTypeDescription);
                var newAccount = await _accountService.CreateAccount(frequency, balance, accountTypeReturn.AccountTypeId, accountTypeDescription);
                var newDisposition = await _dispositionService.SetupDisposition(newCustomer.CustomerId, newAccount.AccountId, dispositionType);
                return Ok("New customer created!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                var token = await _jwtHelper.GetToken("User", email);
                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
