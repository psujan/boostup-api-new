using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Interfaces.Roster;

namespace Boostup.API.Repositories.Roster
{
    public class JobRepository:IJobRepository
    {
        private readonly ApplicationDbContext dbContext;

        public JobRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<JobEmployee>> AddEmployeeToJob(JobEmployeeRequest request)
        {
            List<JobEmployee> jobEmployees = new List<JobEmployee>();
            for (int i = 0; i < request.EmployeeIds.Length; i++)
            {
                var jobEmployee = new Entities.JobEmployee()
                {
                    EmployeeId = request.EmployeeIds[i],
                    JobId = request.JobId
                };
                var row = dbContext.JobEmployee.Add(jobEmployee);
                await dbContext.SaveChangesAsync();
                jobEmployees.Add(jobEmployee);
            }
            return jobEmployees;
        }

        public Task<Jobs?> AddJob()
        {
            throw new NotImplementedException();
        }

        public Task<Jobs?> DeleteJob(int JobId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeDetail>?> ListEmployeeByJob(int JobId)
        {
            throw new NotImplementedException();
        }

        public Task<Jobs?> UpdateJob(int JobId)
        {
            throw new NotImplementedException();
        }
    }
}
