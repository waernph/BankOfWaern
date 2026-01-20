using Bank_of_Waern.Core.Interfaces;

namespace Bank_of_Waern.Core.Services
{
    public class PasswordService : IPasswordService
    {
        public async Task<string> HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 11);
        }

        public async Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }
    }
}
