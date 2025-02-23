using AutoMapper;
using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces.Employee;
using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Repositories.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public EmployeeRepository(ApplicationDbContext dbContext , IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<EmployeeDetailResponse?> AddEmployee(User user, string? phone)
        {
            var employee = new EmployeeDetail()
            {
                UserId = user.Id,
                Contact = phone
            };

            await dbContext.EmployeeDetail.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return mapper.Map<EmployeeDetailResponse>(employee);
        }

        public async Task<EmployeeDetailResponse?> GetById(int id)
        {
            var employee = await dbContext.EmployeeDetail
                            .Include(employee => employee.User)
                            .FirstOrDefaultAsync(x => x.Id == id);
            return employee == null ? throw new Exception("Employee With " + id + " not found") : mapper.Map<EmployeeDetailResponse>(employee);
        }
    }
}
