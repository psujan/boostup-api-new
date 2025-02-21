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

                Console.WriteLine("User Seeding Completed");
            }
        }
    }
}
