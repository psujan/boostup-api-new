namespace Boostup.API.Entities.Dtos.Request
{
    public class JobEmployeeRequest
    {
        public int JobId { get; set; }
        public int[] EmployeeIds { get; set; }
    }
}
