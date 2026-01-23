
using Bank_of_Waern.Data.DTOs;
using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Profiles
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            CreateMap<Loan, LoanDTO>()
                    .ForMember(dest => dest.LoanAmmount,
                    opt => opt.MapFrom(src => src.Amount))
                    .ForMember(dest => dest.payment,
                    opt => opt.MapFrom(src => src.Payments))
                    .ForMember(dest => dest.Months, 
                    opt => opt.MapFrom(src => src.Duration));
                }
    }
}
