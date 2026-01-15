using Bank_of_Waern.Data.Entities;

namespace BrewHub.Core.Interfaces
{
    public interface IJwtHelper
    {
        public Task<string> GetLoggedInUserId();
        public Task<string> GetToken(string role, string emailAddress);
    }
}
