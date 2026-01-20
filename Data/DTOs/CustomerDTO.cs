namespace Bank_of_Waern.Data.DTOs
{
    public class CustomerDTO
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
