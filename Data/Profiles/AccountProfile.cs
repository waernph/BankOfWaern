
using Bank_of_Waern.Data.DTOs;
using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDTO>()
                .ForMember(dest => dest.AccountId,
                opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.Balance,
                opt => opt.MapFrom(src => src.Balance))
                .ForMember(dest => dest.AccountTypeId,
                opt => opt.MapFrom(src => src.AccountTypesId));
        }
    }
}
