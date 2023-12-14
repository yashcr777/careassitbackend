using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.Domain
{
    public class InsurancePlan
    {
        [Key]
        public Guid InsurancePlan_Id { get; set; }
        public Guid? InsuranceCompany_Id { get; set; }
        [Required]
        public string Insurance_Name { get; set; } = string.Empty;
        [Required]
        public string Insurance_Description { get; set; }= string.Empty;
        [Required]
        public string Insurance_Price { get; set; }=string.Empty;
        [Required]
        public string Insurance_Duration { get; set; } = string.Empty;
        
    }
}
