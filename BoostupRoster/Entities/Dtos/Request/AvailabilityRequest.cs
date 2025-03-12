using System.ComponentModel.DataAnnotations;

namespace Boostup.API.Entities.Dtos.Request
{
    public class AvailabilityRequest 
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string Day { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        [Required]
        public bool ForFullDay { get; set; }
    }
}
