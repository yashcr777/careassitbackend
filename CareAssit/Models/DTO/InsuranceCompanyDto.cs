using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.DTO
{
    public class InsuranceCompanyDto
    {
        [Key]
        public Guid InsuranceCompany_Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string InsuranceCompany_Name { get; set; }

        public string InsuranceCompany_Description { get; set; }
    }
}
