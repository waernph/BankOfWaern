using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data;


namespace Bank_of_Waern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IJwtHelper _jwtHelper;
        private readonly IAdminService _adminService;

        public AdminController(IJwtHelper jwtHelper, IAdminService adminService)
        {
            _jwtHelper = jwtHelper;
            _adminService = adminService;
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

