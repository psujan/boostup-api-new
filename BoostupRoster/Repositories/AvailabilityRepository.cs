using Azure.Core;
using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Repositories
{
    public class AvailabilityRepository : BaseRepository<EmployeeAvailability>, IAvailabilityRepository
    {
        public AvailabilityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<EmployeeAvailability?> Add(EmployeeAvailability availability)
        {
            if (availability.ForFullDay)
            {
                var rows = await CheckForDay(availability.EmployeeId, availability.Day);
                if (rows != null)
                {
                    await RemoveRecords(rows);
                }
            }
            return await base.Add(availability);
        }

        public async Task<List<EmployeeAvailability>> CheckForDay(int empId, string day)
        {
            var rows = await dbContext.EmployeeAvailability.Where(r => r.EmployeeId == empId && r.Day == day).ToListAsync();
            return rows;
        }

        public async Task<List<EmployeeAvailability>> RemoveRecords(List<EmployeeAvailability> rows)
        {
            dbContext.EmployeeAvailability.RemoveRange(rows);
            await dbContext.SaveChangesAsync();
            return rows;
        }

        public async Task<EmployeeAvailability?> Update(int id, AvailabilityRequest request)
        {
            var row = await dbContext.EmployeeAvailability.FindAsync(id);
            if (row == null)
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

        public async Task<IEnumerable<EmployeeAvailability>?> GetEmployeeAvailability(int EmpId)
        {
            return await dbContext.EmployeeAvailability.Where(x => x.EmployeeId == EmpId).ToListAsync();
        }

        public async Task<EmployeeAvailability?> FindAvailability(int Id, string from, string to, string day)
        {
            return await dbContext.EmployeeAvailability.Where(r => r.EmployeeId == Id && r.From == from && r.To == to && r.Day == day && r.ForFullDay == false).FirstOrDefaultAsync();
        }

        public async Task<int?> GetTotalDayCount(int Id, string Day)
        {
            return await dbContext.EmployeeAvailability.Where(r => r.EmployeeId == Id && r.Day == Day).CountAsync();
        }

        public async Task<List<GroupedAvailabilityResponse>?> GroupEmployeeAvailability(int Id)
        {
            var result = await dbContext.EmployeeAvailability
                        .Where(e => e.EmployeeId == Id)  // Filter for a specific employee
                        .GroupBy(e => e.Day)               // Group by Day
                        .Select(g => new GroupedAvailabilityResponse()
                        {
                            Day = g.Key,
                            Records = g.ToList()           // Get all records for that day
                        })
                        .ToListAsync();
            return result;
        }

        public async Task<EmployeeAvailability?> FindAvailabilityForFullDay(int id, string day)
        {
            return await dbContext.EmployeeAvailability.Where(r => r.EmployeeId == id && r.Day == day && r.ForFullDay == true).FirstOrDefaultAsync();

        }
    }
}
