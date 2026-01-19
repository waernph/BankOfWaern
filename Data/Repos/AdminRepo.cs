using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using System.Diagnostics.Eventing.Reader;

namespace Bank_of_Waern.Data.Repos
{
    public class AdminRepo : IAdminRepo
    {
        private readonly BankAppDataContext _context;

        public AdminRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<Admin> AdminLogin(string email, string password)
        {
            var admin = _context.Admins.Where(a => a.Email == email).FirstOrDefault();
            if (admin == null)
                throw new Exception("Admin not found");

            else if (admin.Password != password)
                throw new Exception("Wrong email or password");
            else
                return admin;
            
        }

        
    }
}
