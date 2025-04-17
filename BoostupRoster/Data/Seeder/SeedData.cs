using Boostup.API.Entities;
using Microsoft.AspNetCore.Identity;

namespace Boostup.API.Data.Seeder
{
    public class SeedData
    {
        public async static void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                await SeedRoles(dbContext);
                Console.WriteLine("Roles Seeding Done");
            }
        }

        public async static Task SeedRoles(ApplicationDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = "1",
                    Name="SuperAdmin",
                    NormalizedName = "SuperAdmin".ToUpper()
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = "2",
                    Name="Manager",
                    NormalizedName = "Manager".ToUpper()
                },

                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp="3",
                    Name="Employee",
                    NormalizedName = "Employee".ToUpper()
                }
            };

                await dbContext.Roles.AddRangeAsync(roles);
                dbContext.SaveChanges();
            }
        }

        public async static Task SeedUser(IServiceProvider serviceProvider , ApplicationDbContext dbContext)
        {
            if (!dbContext.User.Any())
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                //var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // create a admin user
                var user = new User
                {
                    UserName = "boostupadmin@gmail.com",
                    Email = "boostupadmin@gmail.com",
                    FullName = "Sanjeev Rana",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, "Boostup#2025");

                // Assign the user to a role (optional)
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "SuperAdmin");
                }
                
                //create an employee user
                var employee = new User
                {
                    UserName = "johnmilson@gmail.com",
                    Email = "johnmilson@gmail.com",
                    FullName = "John Milson",
                    EmailConfirmed = true
                };
                result = await userManager.CreateAsync(employee, "John#2025");

                // Assign the user to a role (optional)
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(employee, "Employee");
                }

                var employeeDetail = new EmployeeDetail()
                {
                    UserId = employee.Id
                };
                await dbContext.EmployeeDetail.AddAsync(employeeDetail);
                await dbContext.SaveChangesAsync();
                Console.WriteLine("User Seeding Completed");
            }
        }

        public  async static Task SeedJobs(ApplicationDbContext dbContext)
        {
            if (!dbContext.Jobs.Any())
            {
                var jobs = new List<Jobs>()
                {
                    new Jobs()
                    {
                        Title = "Pool Cleaning - Macquire Park",
                        StartTime = "6:00 AM",
                        EndTime = "10:00 AM",

                    },
                    new Jobs()
                    {
                        Title = "Bar Cleaning - Wynard",
                        StartTime = "6:00 AM",
                        EndTime = "11:00 AM",
                    },
                    new Jobs()
                    {
                        Title =  "Office Cleaning - Chatswood",
                        StartTime = "7:00 PM",
                        EndTime = "11:30 PM",
                    },
                    new Jobs()
                    {
                        Title = "Warehouse Cleaning Manly - Morning",
                        StartTime = "6:00 AM",
                        EndTime = "2: 00 PM",
                    },
                    new Jobs()
                    {
                        Title = "Warehouse Cleaning Manly - Afternoon",
                        StartTime = "2:00 PM",
                        EndTime = "10: 00 PM",
                    },
                    new Jobs()
                    {
                        Title = "Warehouse Cleaning Manly - Night",
                        StartTime = "10:00 PM",
                        EndTime = "6: 00 AM",
                    }
                };
                dbContext.Jobs.AddRange(jobs);
                await dbContext.SaveChangesAsync();
                Console.WriteLine("Jobs Seeding Completed");
            }
            
        }

        public async static Task SeedLeaveTypes(ApplicationDbContext dbContext)
        {
            if (!dbContext.LeaveType.Any())
            {
                var leaves = new List<LeaveType>
                {
                    new LeaveType()
                    {
                        Title="Sick Leave",
                        Days= 10,
                    },
                    new LeaveType()
                    {
                        Title="Annual Leave",
                        Days= 10,
                    },
                    new LeaveType()
                    {
                        Title="Parental Leave",
                        Days= 30,
                    },
                    new LeaveType()
                    {
                        Title="Long Service Leave",
                        Days= 30,
                    },
                    new LeaveType()
                    {
                        Title="Unpaid Leave",
                        Days= 30,
                    },
                     new LeaveType()
                     {
                        Title="Compassionate Leave",
                        Days= 30,
                     },
                      new LeaveType()
                     {
                        Title="Community Service Leave",
                        Days= 10,
                     },
                };

                await dbContext.LeaveType.AddRangeAsync(leaves);
                dbContext.SaveChanges();
            }
        }
    }
}
       