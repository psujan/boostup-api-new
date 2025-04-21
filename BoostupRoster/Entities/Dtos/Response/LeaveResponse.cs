namespace Boostup.API.Entities.Dtos.Response
{
    public class LeaveResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
        public bool? ForSingleDay { get; set; }
        public bool? IsPaidLeave { get; set; }
        public string? Notes { get; set; } // For Reason and any other leave details
        public string Status { get; set; } = "Pending"; // Approved, Rejected
        public string? RejectReason { get; set; }
        public int? LeaveTypeId { get; set; }
        public LeaveType? LeaveType { get; set; }
        public EmployeeBasicResponse Employee { get; set; }
    }
}
