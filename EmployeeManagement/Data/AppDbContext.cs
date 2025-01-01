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
        public DbSet<User> User {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Apply unique constraint on the UserName field
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();  // This makes the UserName field unique in the database

            // Seed a default row in the User table
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1, // Primary Key
                    Email = "viraj@yopmail.com", // Unique email
                    PasswordHash = "73l8gRjwLftklgfdXT+MdiMEjJwGPVMsyVxe16iYpk8=", // Replace with hashed password
                    UserName = "Viraj" // Optional
                }
            );
        }

    }
}
