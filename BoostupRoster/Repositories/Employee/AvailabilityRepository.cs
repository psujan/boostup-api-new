using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Interfaces.Employee;

namespace Boostup.API.Repositories.Employee
{
    public class AvailabilityRepository : BaseRepository<EmployeeAvailability> , IAvailabilityRepository
    {
        public AvailabilityRepository(ApplicationDbContext dbContext):base(dbContext) { }  
    }
}
