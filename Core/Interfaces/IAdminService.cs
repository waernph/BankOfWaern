using Bank_of_Waern.Data.DTOs;
using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Core.Interfaces
{
    public interface IAdminService
    {
        public Task<Admin> AdminLogin(string email, string password);

    }
}
