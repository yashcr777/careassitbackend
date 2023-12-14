using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.DTO
{
    public class AddInsuranceCompanyDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? InsuranceCompany_Name { get; set; }

        public string? InsuranceCompany_Description { get; set; }
    }
}
