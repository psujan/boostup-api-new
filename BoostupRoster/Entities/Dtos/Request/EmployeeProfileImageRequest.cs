namespace Boostup.API.Entities.Dtos.Request
{
    public class EmployeeProfileImageRequest
    {
        public int EmployeeId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
