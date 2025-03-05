namespace Boostup.API.Entities
{
    public class Roster:Base
    {
        public DateOnly Date { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? WorkHours { get; set; }
        public string? Notes { get; set; }
        public int? JobId { get; set; }
        public int EmployeeId { get; set; }
        public Jobs? Job { get; set; }
        public EmployeeDetail Employee {  get; set; }
    }
}
