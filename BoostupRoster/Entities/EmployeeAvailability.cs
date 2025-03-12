namespace Boostup.API.Entities
{
    public class EmployeeAvailability: Base
    {
        public int EmployeeId { get; set; }
        public string Day { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool ForFullDay { get; set; } = false;
        public EmployeeDetail EmployeeDetail { get; set; }
    }
}
