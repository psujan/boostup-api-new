using System.ComponentModel.DataAnnotations;

namespace Boostup.API.Entities.Dtos.Request
{
    public class ClockoutRequest
    {
        [Required]
        public int TimeSheetId { get; set; }
        [Required]
        public string ClockOut {  get; set; }
        [Required]
        public string TotalHours { get; set; }

    }
}
