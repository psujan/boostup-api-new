using AutoMapper;
using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Repositories
{
    public class JobRepository : BaseRepository<Jobs> , IJobRepository
    {
        private readonly IMapper mapper;

        public JobRepository(ApplicationDbContext dbContext, IMapper mapper):base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<List<JobEmployee>> AddEmployeeToJob(JobEmployeeRequest request)
        {
            List<JobEmployee> jobEmployees = new List<JobEmployee>();
            for (int i = 0; i < request.EmployeeIds.Length; i++)
            {
                var jobEmployee = new JobEmployee()
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

        public async Task<IEnumerable<EmployeeBasicResponse>?> ListEmployeeByJob(int JobId)
        {
            var rows = await dbContext.Jobs.Where(job => job.Id == JobId)
                .SelectMany(job => job.JobEmployee).
                Select(je => new EmployeeBasicResponse()
                {
                    EmployeeId = je.EmployeeId,
                    EmployeeName = je.Employee.User.FullName
                })
                .ToListAsync();
            //var mappedData = mapper.Map<IEnumerable<EmployeeBasicResponse>>(rows);
            return rows;
        }

        public async Task<Jobs?> Update(int JobId , JobRequest request)
        {
            var job = await this.GetById(JobId);
            if(job == null)
            {
                throw new Exception("Job Not Found");
            }
            job.Title = request.Title;
            job.Notes = request.Notes;
            job.StartTime = request.StartTime;
            job.EndTime = request.EndTime;
            job.UpdatedAt = DateTime.Now;
            job.JobAddress = request.JobAddress;
            await dbContext.SaveChangesAsync();
            return job;
        }
    }
}
