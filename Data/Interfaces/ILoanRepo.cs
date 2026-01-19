using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface ILoanRepo
    {
        public Task<Loan> ApplyForLoad(decimal amount, int duration, int accountId, int customerId, decimal payments);
    }
}
