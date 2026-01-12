using Bank_of_Waern.Data.Entities;

namespace BrewHub.Core.Interfaces
{
    public interface IJwtGetter
    {
        public Task<string> GetLoggedInUserId();
    }
}
