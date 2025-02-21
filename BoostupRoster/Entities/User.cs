using Microsoft.AspNetCore.Identity;

namespace Boostup.API.Entities
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Contact {  get; set; }  
        public string Status { get; set; } = "Active";
        public virtual EmployeeDetail? EmployeeDetail { get; set; }

    }
}
