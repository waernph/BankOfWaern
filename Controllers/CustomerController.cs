using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Core.Services;
using BrewHub.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank_of_Waern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IJwtGetter _jwtGetter;

        public CustomerController(ICustomerService customerService, IJwtGetter jwtGetter)
        {
            _customerService = customerService;
            _jwtGetter = jwtGetter;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createCustomer")]
        public async Task<IActionResult> CreateCustomer(string firstName, string lastName, string gender, string street, string city, string zip, string country, string countryCode, string birthday, string emailAdress, string phoneCountryCode, string phoneNumber)
        {
            try
            {
                var newCustomer = await _customerService.CreateCustomer(firstName, lastName, gender, street, city, zip, country, countryCode, birthday, emailAdress, phoneCountryCode, phoneNumber);
                return Ok(newCustomer);
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
                var token = await _customerService.GenerateToken(user);
                return Ok(new { token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
