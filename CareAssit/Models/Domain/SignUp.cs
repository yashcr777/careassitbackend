using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.Domain
{
    public class SignUp
    {
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string User_Name { get; set; }
        
    }
}
