using AutoMapper;
using Customer.API.Models.Domain;
using Customer.API.Models.DTO;

namespace Customer.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CustomerDB, CustomerDTO>().ReverseMap();
            CreateMap<AddCustomerRequestDto, CustomerDB>().ReverseMap();
            CreateMap<UpdateCustomerRequestDto, CustomerDB>().ReverseMap();
        }
    }
}
