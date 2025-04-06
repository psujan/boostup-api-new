namespace Boostup.API.Entities
{
    public class Timesheet:Base
    {
        public int EmployeeId { get; set; }

        public int? RosterId { get; set; }

        public int? JobId { get; set; }

        public string? ClockIn { get; set; }

        public string? ClockOut { get; set; }

        public string? TotalHous { get; set; }

        public DateOnly Date { get; set; }

        //Navigational Properties
        public EmployeeDetail? Employee {  get; set; }
        public Roster? Roster { get; set; }
        public Jobs? Job { get; set; }  
    }


}
