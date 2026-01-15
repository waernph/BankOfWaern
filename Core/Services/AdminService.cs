using AutoMapper;
using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.DTOs;
using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using BrewHub.Core.Interfaces;

namespace Bank_of_Waern.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _repo;
        private readonly IJwtHelper _jwtHelper;
        private readonly ICustomerRepo _customerRepo;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepo repo, IJwtHelper jwtHelper, ICustomerRepo customerRepo, IMapper mapper)
        {
            _repo = repo;
            _jwtHelper = jwtHelper;
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task<Admin> AdminLogin(string email, string password)
        {
            return await _repo.AdminLogin(email, password);
        }

        public async Task ApplyForLoan(decimal amount, int duration, int accountId, int customerId)
        {
            decimal payments = Math.Round((amount / duration), 2);
            var loanGranted = await _repo.ApplyForLoad(amount, duration, accountId, customerId, payments);
        }
    }
}
