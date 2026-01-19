using Bank_of_Waern.Data.DTOs;

namespace Bank_of_Waern.Core.Interfaces
{
    public interface ILoanService
    {
        public Task<LoanDTO> ApplyForLoan(decimal amount, int duration, int accountId, int customerId);
    }
}
