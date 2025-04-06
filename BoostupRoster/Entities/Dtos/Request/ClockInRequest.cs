using System.ComponentModel.DataAnnotations;

namespace Boostup.API.Entities.Dtos.Request
{
    public class ClockInRequest
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int RosterId { get; set; }

        [Required]
        public int JobId { get; set; }
        [Required]
        public string Date {  get; set; }

        [Required]
        public string ClockIn {  get; set; }
    }
}
