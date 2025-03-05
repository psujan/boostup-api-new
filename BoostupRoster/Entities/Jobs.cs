namespace Boostup.API.Entities
{
    public class Jobs:Base
    {
        public string Title {  get; set; }
        public string? Notes {  get; set; }
        public string? StartTime {  get; set; }
        public string? EndTime { get; set; }
        public string? JobAddress { get; set; }
        //public ICollection<EmployeeDetail>?  Employees { get; set; }
        public ICollection<JobEmployee>? JobEmployee { get; set; }
    }
}
