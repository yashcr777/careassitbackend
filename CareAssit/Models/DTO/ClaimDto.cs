using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.DTO
{
    public class ClaimDto
    {
        [Key]
        public Guid Claim_Id { get; set; }
        public Guid? User_Id { get; set; }
        public Guid? Invoice_Id { get; set; }
        public Guid? InsuranceCompant_Id { get; set; }
        [Required]
        public string User_Name { get; set; }
        [Required]
        public string Dob { get; set; }
        [Required]
        public string Address { get; set; }
        public string DateOfService { get; set; }
        public string Treatment { get; set; }
        public string Diagnosis { get; set; }
        public long Claim_Amount { get; set; }
        public long Invoice_Amount { get; set; }
        public Status2 status2 { get; set; } = Status2.NotClaimed;

        public enum Status2
        {
            Claimed,
            NotClaimed
        }
    }
    public class UpdateClaimDto
    {
        public Guid Claim_Id { get; set;}
    }
}
