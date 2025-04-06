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
        public DbSet<EmployeeAvailability> EmployeeAvailability { get; set; }
        public DbSet<EmployeeProfileImage> EmployeeProfileImage { get; set; }
        public DbSet<Timesheet> Timesheet{ get; set; }
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
                .WithOne(r => r.Employee)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeDetail>()
                .HasOne(e => e.Image)
                .WithOne(i => i.Employee)
                .HasForeignKey<EmployeeProfileImage>(e => e.EmployeeId)
                .IsRequired(false);

            modelBuilder.Entity<Leave>()
                .HasOne(l => l.LeaveType)
                .WithMany();

            modelBuilder.Entity<Leave>()
                .HasOne(l => l.Employee)
                .WithMany();

            modelBuilder.Entity<EmployeeAvailability>()
                .HasOne(a => a.EmployeeDetail)
                .WithMany(emp => emp.Availabilities)
                .HasForeignKey(a => a.EmployeeId);

            modelBuilder.Entity<Timesheet>()
                .HasOne(t => t.Employee)
                .WithMany(e => e.Timesheets)
                .HasForeignKey(t => t.EmployeeId);

            modelBuilder.Entity<Timesheet>()
               .HasOne(t => t.Roster)
               .WithMany(r => r.Timesheets)
               .HasForeignKey(t => t.RosterId)
               .IsRequired(true);

            modelBuilder.Entity<Timesheet>()
               .HasOne(t => t.Job)
               .WithMany(j => j.Timesheets)
               .HasForeignKey(t => t.JobId)
               .IsRequired(false);

        }
    }
}
