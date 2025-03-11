namespace Boostup.API.Entities.Dtos.Request
{
    public class LeaveUpdateRequest
    {
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
        public bool? ForSingleDay { get; set; }
        public bool? IsPaidLeave { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = "Pending";
        public string? RejectReason { get; set; }
    }
}
