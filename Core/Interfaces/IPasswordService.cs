namespace Bank_of_Waern.Core.Interfaces
{
    public interface IPasswordService
    {
        public Task<string> HashPassword(string password);
        public Task<bool> VerifyPassword(string password, string hashedPassword);
    }
}
