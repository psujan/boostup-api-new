using Boostup.API.Entities;

namespace Boostup.API.Interfaces.Employee
{
    public interface IAvailabilityRepository
    {
        Task<EmployeeAvailability?> Add(EmployeeAvailability availability);
        Task<EmployeeAvailability?> Delete(int id);
        Task<EmployeeAvailability?> Update(int id , EmployeeAvailability availability);
    }
}
