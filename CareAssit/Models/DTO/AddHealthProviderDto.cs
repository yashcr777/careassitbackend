using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.DTO
{
    public class AddHealthProviderDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? HelathProvider_Name { get; set; }
        public string? CertificateNumber { get; set; }
    }
}
