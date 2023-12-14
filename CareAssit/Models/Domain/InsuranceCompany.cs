using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.Domain
{
    public class InsuranceCompany
    {
        [Key]
        public Guid InsuranceCompany_Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? InsuranceCompany_Name { get; set; }

        public string? InsuranceCompany_Description { get; set; }
    }
}
