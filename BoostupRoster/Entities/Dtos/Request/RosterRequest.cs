namespace Boostup.API.Entities.Dtos.Request
{
    public class RosterRequest
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string WorkHours { get; set; }
        public int JobId { get; set; }
        public string? Notes { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly Date { get; set; }
    }
}
