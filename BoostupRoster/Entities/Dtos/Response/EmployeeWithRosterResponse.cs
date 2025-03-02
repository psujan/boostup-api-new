namespace Boostup.API.Entities.Dtos.Response
{
    public class EmployeeWithRosterResponse : Base
    {
        public int Id {  get; set; }
        public string? EmploymentType { get; set; }
        public UserResponse User { get; set; }
        public ICollection<RosterBasicResponse>? RosterItems { get; set; }
    }
}
