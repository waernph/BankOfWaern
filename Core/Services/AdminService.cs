using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using BrewHub.Core.Interfaces;

namespace Bank_of_Waern.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _repo;
        private readonly IJwtHelper _jwtHelper;

        public AdminService(IAdminRepo repo, IJwtHelper jwtHelper)
        {
            _repo = repo;
            _jwtHelper = jwtHelper;
        }

        public async Task<Admin> AdminLogin(string email, string password)
        {
            return await _repo.AdminLogin(email, password);
        }
    }
}
