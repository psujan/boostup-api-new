using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces
{
    public interface IJobRepository: IBaseRepository<Jobs>
    {
        Task<List<JobEmployee>> AddEmployeeToJob(JobEmployeeRequest request);
        Task<Jobs?> Update(int JobId, JobRequest request);
        Task<IEnumerable<EmployeeBasicResponse>?> ListEmployeeByJob(int JobId);
        Task<PaginatedResponse<Jobs>?> GetPaginated(int pageNumber, int pageSize);
    }
}
