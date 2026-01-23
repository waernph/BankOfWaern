
using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.DTOs;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepo _repo;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<LoanDTO> ApplyForLoan(decimal amount, int duration, int accountId, int customerId)
        {
            decimal payments = Math.Round((amount / duration), 2);
            var loanGranted = await _repo.ApplyForLoad(amount, duration, accountId, customerId, payments);
            return _mapper.Map<LoanDTO>(loanGranted);
        }
    }
}
