namespace Boostup.API.Entities.Dtos.Request
{
    public class LeaveFilterRequest
    {
        public int[]? LeaveTypeIds { get; set; }
        public bool IsAllLeaveType { get; set; } = true; 
        public string? Year { get; set; }
        public bool IsAllEmployee { get; set; } = true;
        public int[]? EmployeeIds { get; set; }
        public string? Status { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
