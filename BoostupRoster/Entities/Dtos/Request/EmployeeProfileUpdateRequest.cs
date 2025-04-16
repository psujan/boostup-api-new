namespace Boostup.API.Entities.Dtos.Request
{
    public class EmployeeProfileUpdateRequest
    {
        public int EmployeeId { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? BirthCountry { get; set; }
        public DateOnly? DOB { get; set; }
        public string? Gender { get; set; }
        public bool IsTaxFree { get; set; } = false;
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? VerificationDocument { get; set; }
        public string? TFN { get; set; }
        public string? ABN { get; set; }
        public DateTime? JoinedDate { get; set; }
        public string? EmploymentType { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; } = "Active";
    }
}
