using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces.Roster
{
    public interface IJobRepository
    {
        Task<List<JobEmployee>> AddEmployeeToJob(JobEmployeeRequest request);
        Task<Jobs?> AddJob();
        Task<Jobs?> UpdateJob(int JobId);
        Task<Jobs?> DeleteJob(int JobId);
        Task<IEnumerable<EmployeeBasicResponse>?> ListEmployeeByJob(int JobId);
    }
}
