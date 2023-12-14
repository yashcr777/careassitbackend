using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.DTO
{
    public class AddInsurancePlanDto
    {
        
        public Guid? InsuranceCompany_Id { get; set; }
        public string Insurance_Name { get; set; }
        public string Insurance_Description { get; set; }
        public string Insurance_Price { get; set; }
        public string Insurance_Duration { get; set; }
    }
}
