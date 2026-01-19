using Bank_of_Waern.Data.Entities;

namespace BrewHub.Core.Interfaces
{
    public interface IJwtHelper
    {
        public Task<int> GetLoggedInCustomerId();
        public Task<string> GetLoggedInEmail();
        public Task<string> GetToken(string role, string? email, Customer? user);


    }
}
