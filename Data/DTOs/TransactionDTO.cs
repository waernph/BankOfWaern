using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.DTOs
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }

        public DateOnly Date { get; set; }

        public string Type { get; set; } = null!;

        public string Operation { get; set; } = null!;

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public string? Bank { get; set; }

        public string? Account { get; set; }

    }
}