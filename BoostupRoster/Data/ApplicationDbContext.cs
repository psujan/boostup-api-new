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
        public DbSet<JobEmployee> JobEmployee { get; set; }
        public DbSet<Roster> Roster { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<Leave> Leave { get; set; }
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

            modelBuilder.Entity<JobEmployee>()
                .HasOne(je => je.Job)
                .WithMany(j => j.JobEmployee)
                .HasForeignKey(je => je.JobId);

            modelBuilder.Entity<JobEmployee>()
                .HasOne(je => je.Employee)
                .WithMany(e => e.JobEmployee)
                .HasForeignKey(je => je.EmployeeId);  
            
            modelBuilder.Entity<Roster>()
                .HasOne(r => r.Employee)
                .WithMany(e => e.Rosters)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EmployeeDetail>()
                .HasMany(e => e.Rosters)
                .WithOne(r  => r.Employee)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Leave>()
                .HasOne(l => l.LeaveType)
                .WithMany();

            modelBuilder.Entity<Leave>()
                .HasOne(l => l.Employee)
                .WithMany();
          
        }
    }
}
