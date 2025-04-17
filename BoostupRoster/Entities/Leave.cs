namespace Boostup.API.Entities
{
    public class Leave:Base
    {
        public int EmployeeId { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
        public bool? ForSingleDay { get; set; }
        public bool? IsPaidLeave { get; set; }
        public string? Notes { get; set; } // For Reason and any other leave details
        public string Status { get; set; } = "Pending"; // Approved, Rejected
        public string? RejectReason {get; set; }
        public int? LeaveTypeId { get; set; }
        public int? RosterId { get; set; }
        public LeaveType? LeaveType { get; set; }
        public EmployeeDetail Employee { get; set; }
        public Roster? Roster { get; set; }
    }
}
