using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.Domain
{
    public class HealthProvider
    {
        [Key]
        public Guid Health_Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? HelathProvider_Name { get; set; }
        public string? CertificateNumber { get; set; }
    }
}
