using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Interfaces.Employee;

namespace Boostup.API.Repositories.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<EmployeeDetail?> AddEmployee(User user, string? phone)
        {
            var employee = new EmployeeDetail()
            {
                UserId = user.Id,
                Contact = phone
            };

            await dbContext.EmployeeDetail.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return employee;
        }
    }
}
