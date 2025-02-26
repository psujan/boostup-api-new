namespace Boostup.API.Entities
{
    public class Roster
    {
        public DateOnly Date { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? WorkHours { get; set; }
        public int? JobId { get; set; }
        public Jobs? Job { get; set; }
        public string? Notes { get; set; }
        public ICollection<RosterEmployee> Employees { get; set; }
    }
}
