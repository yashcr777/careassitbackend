using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.Domain
{
    public class User
    { 
        [Key]
        public Guid User_Id { get; set; }
        [Required]
        public string Name { get; set; }
        [MinLength(10)]
        public string? ContactNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Dob { get; set; }
        public string? Gender { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Blood_Group { get; set; }
    }
}
