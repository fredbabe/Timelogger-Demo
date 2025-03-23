using Microsoft.EntityFrameworkCore;
using timelogger_web_api.Models.Entities;

namespace timelogger_web_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Registration> Registrations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define precision for HoursWorked to avoid truncation
            modelBuilder.Entity<Registration>()
                .Property(r => r.HoursWorked)
                .HasPrecision(10, 2); // Allows up to 10 digits, 2 of which are after the decimal point

            // Define FK constraints explicitly if needed
            modelBuilder.Entity<Registration>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(r => r.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
