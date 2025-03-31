
using Boostup.API.Entities.Common;

namespace Boostup.API.Entities
{
    public class EmployeeProfileImage : FileModel
    {
        public int EmployeeId { get; set; }

        public EmployeeDetail Employee {  get; set; }
    }
}
