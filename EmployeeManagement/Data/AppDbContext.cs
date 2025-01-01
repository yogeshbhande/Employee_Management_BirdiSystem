using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<FileUploadEmployeeMapping> EmployeeFileMappings { get; set; }
        public DbSet<User> User {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Apply unique constraint on the UserName field
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();  // This makes the UserName field unique in the database
        }

    }
}
