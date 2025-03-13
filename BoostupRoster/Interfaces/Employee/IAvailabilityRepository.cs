using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;

namespace Boostup.API.Interfaces.Employee
{
    public interface IAvailabilityRepository
    {
        Task<EmployeeAvailability?> Add(EmployeeAvailability availability);
        Task<EmployeeAvailability?> Delete(int id);
        Task<EmployeeAvailability?> Update(int id, AvailabilityRequest request);
        Task<IEnumerable<EmployeeAvailability>?> GetEmployeeAvailability(int EmpId);
    }
}
