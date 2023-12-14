using System.ComponentModel.DataAnnotations;

namespace CareAssit.Models.DTO
{
    public class UpdateInsurancePlanDto
    {
        public string Insurance_Name { get; set; } = string.Empty;
        public string Insurance_Description { get; set; } = string.Empty;
        public string Insurance_Price { get; set; } = string.Empty;
        public string Insurance_Duration { get; set; } = string.Empty;
    }
}
