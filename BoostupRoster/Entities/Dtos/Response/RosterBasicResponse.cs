namespace Boostup.API.Entities.Dtos.Response
{
    public class RosterBasicResponse
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? WorkHours { get; set; }
        public string? Notes { get; set; }
        public JobResponseBasic? Job { get; set; }
    }
}
