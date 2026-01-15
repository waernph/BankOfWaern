using AutoMapper;
using Bank_of_Waern.Core.Services;
using Bank_of_Waern.Data.DTOs;
using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Profiles
{
    public class CustomerProfile : Profile
    {
       public CustomerProfile() 
        {
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.Givenname} {src.Surname}".Trim()))
                .ForMember(dest => dest.Address,
                opt => opt.MapFrom(src => src.Streetaddress));
        }
    }
}
