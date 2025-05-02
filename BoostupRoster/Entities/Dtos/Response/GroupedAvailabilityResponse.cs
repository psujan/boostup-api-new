namespace Boostup.API.Entities.Dtos.Response
{
    public class GroupedAvailabilityResponse
    {
        public string Day { get; set; }
        public List<EmployeeAvailability> Records { get; set; }
    }
}
