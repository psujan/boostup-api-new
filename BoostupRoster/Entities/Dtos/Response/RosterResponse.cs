namespace Boostup.API.Entities.Dtos.Response
{
    public class RosterResponse
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? WorkHours { get; set; }
        public string? Notes { get; set; }
        public int? JobId { get; set; }
        public int EmployeeId { get; set; }
        public JobResponseBasic? Job { get; set; }
        public EmployeeBasicResponse Employee { get; set; }
        public ICollection<LeaveResponse>? Leaves { get; set; }
        
    }
}
