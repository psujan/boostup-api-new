using AutoMapper;
using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces.Roster;
using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Repositories.Roster
{
    public class JobRepository:IJobRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public JobRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
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

        public async Task<IEnumerable<EmployeeBasicResponse>?> ListEmployeeByJob(int JobId)
        {
            var rows =  await dbContext.Jobs.Where(job => job.Id == JobId)
                .SelectMany(job => job.JobEmployee).
                Select(je=> new EmployeeBasicResponse()
                {
                    EmployeeId = je.EmployeeId,
                    EmployeeName = je.Employee.User.FullName
                })
                .ToListAsync();
            //var mappedData = mapper.Map<IEnumerable<EmployeeBasicResponse>>(rows);
            return rows;
        }

        public Task<Jobs?> UpdateJob(int JobId)
        {
            throw new NotImplementedException();
        }
    }
}
