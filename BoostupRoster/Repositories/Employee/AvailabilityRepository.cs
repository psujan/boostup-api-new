using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Interfaces.Employee;
using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Repositories.Employee
{
    public class AvailabilityRepository : BaseRepository<EmployeeAvailability> , IAvailabilityRepository
    {
        public AvailabilityRepository(ApplicationDbContext dbContext):base(dbContext) { 
        }

        public async Task<EmployeeAvailability?> Update(int id, AvailabilityRequest request)
        {
            
            var row = await dbContext.EmployeeAvailability.FindAsync(id);
            if(row ==  null)
            {
                throw new Exception("Row Not Found");
            }
            row.ForFullDay = request.ForFullDay;
            row.Day = request.Day;
            row.From = request.From;
            row.To = request.To;
            row.EmployeeId = request.EmployeeId;
            await dbContext.SaveChangesAsync();
            return row;
        }
    }
}
