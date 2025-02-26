using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Entities
{
    public class JobEmployee
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int EmployeeId { get; set; }
        public Jobs Job { get; set; }
        public EmployeeDetail Employee { get; set; }
    }
}
