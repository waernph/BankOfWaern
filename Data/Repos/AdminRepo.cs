using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Data.Repos
{
    public class AdminRepo : IAdminRepo
    {
        private readonly BankAppDataContext _context;
        private readonly IPasswordService _passwordService;

        public AdminRepo(BankAppDataContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<Admin> AdminLogin(string email)
        {
            var admin = _context.Admins.Where(a => a.Email == email).FirstOrDefault();
            
            if (admin == null)
                throw new Exception("Admin not found");

            
            else
                return admin;  
        }

        
    }
}
