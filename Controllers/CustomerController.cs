using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Core.Services;
using BrewHub.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _customerService.GetAllCustomers());
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
