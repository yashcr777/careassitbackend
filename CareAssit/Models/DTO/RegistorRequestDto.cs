using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.DTO
{
    public class RegistorRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
