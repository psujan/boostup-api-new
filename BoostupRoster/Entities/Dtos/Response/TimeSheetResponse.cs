namespace Boostup.API.Entities.Dtos.Response
{
    public class TimeSheetResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public int? RosterId { get; set; }

        public int? JobId { get; set; }

        public string? ClockIn { get; set; }

        public string? ClockOut { get; set; }

        public string? TotalHours { get; set; }

        public DateOnly Date { get; set; }
    }
}
