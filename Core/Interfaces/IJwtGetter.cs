namespace BrewHub.Core.Interfaces
{
    public interface IJwtGetter
    {
        public Task<int> GetLoggedInUserId();
    }
}
