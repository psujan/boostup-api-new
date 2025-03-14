using System.ComponentModel.DataAnnotations;

namespace Boostup.API.Entities.Dtos.Request
{
    public class JobRequest
    {
        [Required]
        public string Title { get; set; }
        public string? Notes { get; set; }

        [Required]
        public string? StartTime { get; set; }

        [Required]
        public string? EndTime { get; set; }
        public string? JobAddress { get; set; }
    }
}
