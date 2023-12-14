using AutoMapper;
using CareAssit.Models.Domain;
using CareAssit.Models.DTO;

namespace CareAssit.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<AddInsurancePlanDto,InsurancePlan>().ReverseMap();
            CreateMap<InsurancePlan,InsurancePlanDto>().ReverseMap();
            //CreateMap<UpdateInsurancePlanDto,InsurancePlan>().ReverseMap();
            CreateMap<HealthProvider, HealthProviderDto>().ReverseMap();
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<Request, RequestDto>().ReverseMap();
            CreateMap<InsuranceCompany,InsuranceCompanyDto>().ReverseMap();
            CreateMap<Claim,ClaimDto>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
        }
    }
}
