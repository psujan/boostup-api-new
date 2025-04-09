namespace Boostup.API.Entities.Dtos.Request
{
    public class RosterFilterRequest
    {
        public int? JobId { get; set; }
        public int[]? EmployeeIds { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
