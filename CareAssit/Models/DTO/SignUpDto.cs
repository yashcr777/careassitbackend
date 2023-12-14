using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.DTO
{
    public class SignUpDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string User_Name { get; set; }
    }
    public class AddSignUpDto
    { 
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string User_Name { get; set; }
        
    }
}
