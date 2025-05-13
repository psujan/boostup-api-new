namespace Boostup.API.Entities.Dtos.Response
{
    public class EmployeeDetailResponse:Base
    {
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
        public string? Status {  get; set; }
        public DateOnly? JoinedDate { get; set; }
        public string? EmploymentType { get; set; }
        public string? Notes { get; set; }
        public string UserId { get; set; }
        public UserResponse User { get; set; }
        public EmployeeImageResponse? Image { get; set; }
        public ICollection<EmployeeAvailability>? Availabilities { get; set; }
    }
}
