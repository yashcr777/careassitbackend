using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.DTO
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        [MinLength(10)]
        public string ContactNumber { get; set; }
        public string Dob { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Blood_Group { get; set; }
    }
}
