using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces
{
    public interface IAvailabilityRepository
    {
        Task<EmployeeAvailability?> Add(EmployeeAvailability availability);
        Task<EmployeeAvailability?> Delete(int id);
        Task<EmployeeAvailability?> Update(int id, AvailabilityRequest request);
        Task<IEnumerable<EmployeeAvailability>?> GetEmployeeAvailability(int EmpId);
        Task<EmployeeAvailability?> FindAvailability(int Id, string from , string to);
        Task<int?> GetTotalDayCount(int Id, string Day);
        Task<List<GroupedAvailabilityResponse>?> GroupEmployeeAvailability(int Id);

    }
}
