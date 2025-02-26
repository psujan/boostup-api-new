using Boostup.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Net.Sockets;

namespace Boostup.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetail { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
       // public DbSet<JobEmployee> JobEmployee { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeDetail>(x =>
            {
                x.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1000, 1);
            });



            //modelBuilder.Entity<Jobs>()
            //    .HasMany(j => j.Employees)
            //    .WithMany(e => e.Jobs)
            //    .UsingEntity(
            //        "JobEmployee",
            //        l => l.HasOne(typeof(EmployeeDetail)).WithMany().HasForeignKey("EmployeeId").HasPrincipalKey("Id"),
            //        r => r.HasOne(typeof(Jobs)).WithMany().HasForeignKey("JobId").HasPrincipalKey("Id"),
            //        j => j.HasKey("JobId", "EmployeeId"));

            modelBuilder.Entity<JobEmployee>()
                .HasOne(je => je.Job)
                .WithMany(j => j.JobEmployee)
                .HasForeignKey(je => je.JobId);

            modelBuilder.Entity<JobEmployee>()
                .HasOne(je => je.Employee)
                .WithMany(e => e.JobEmployee)
                .HasForeignKey(je => je.EmployeeId);
        }

    }
}
