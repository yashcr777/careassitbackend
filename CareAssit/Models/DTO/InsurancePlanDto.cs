using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.DTO
{
    public class InsurancePlanDto
    {
        [Key]
        [Required]
        public Guid InsurancePlan_Id { get; set; }
        public Guid? InsuranceCompany_Id { get; set; }
        public string Insurance_Name { get; set; } = string.Empty;
        public string Insurance_Description { get; set; } = string.Empty;
        public string Insurance_Price { get; set; } = string.Empty;
        public string Insurance_Duration { get; set; } = string.Empty;
    }
}
