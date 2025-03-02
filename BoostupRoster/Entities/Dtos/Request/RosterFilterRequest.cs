namespace Boostup.API.Entities.Dtos.Request
{
    public class RosterFilterRequest
    {
        public int JobId { get; set; }
        public int[] EmployeeIds { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
