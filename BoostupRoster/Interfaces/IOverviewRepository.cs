using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces
{
    public interface IOverviewRepository
    {
        Task<EmployeeOverviewResponse> GetEmployeeDashboardOverview(int id);
        Task<object> GetAdminDashboardOverview();
    }
}
