using Microsoft.EntityFrameworkCore;
using System;
using UserManagement.Model;

namespace UserManagement.Repository
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext()
        {
        }

        public UserManagementContext(DbContextOptions<UserManagementContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAddress> UserAddress { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAddress>().HasData(
                new UserAddress() { AddressId = 1, Country = "USA", City = "New York", PinCode = "12345", State = "USA State", Street = "Test Street Address USA" },
                 new UserAddress() { AddressId = 2, Country = "Oman", City = "Muscat", PinCode = "445778", State = "Oman State", Street = "Test Street Address Oman" },
                  new UserAddress() { AddressId = 3, Country = "UAE", City = "Dubai", PinCode = "98766", State = "UAE State", Street = "Test Street Address UAE" });
            modelBuilder.Entity<User>().HasData(
                new User() {UserId=1, Name = "John", Designation = "Developer", JoiningDate = DateTime.Now.AddDays(-1), AddressId = 1,  ImagePath = "" },
                 new User() { UserId = 2, Name = "Mark", Designation = "Project Manger", JoiningDate = DateTime.Now.AddDays(-2), AddressId = 2, ImagePath = "" },
                  new User() { UserId = 3, Name = "Smith", Designation = "Team Lead", JoiningDate = DateTime.Now.AddDays(-3), AddressId = 3, ImagePath = "" },
                   new User() { UserId = 4, Name = "Emma", Designation = "Developer", JoiningDate = DateTime.Now.AddMonths(-1), AddressId = 1, ImagePath = "" },
                 new User() { UserId = 5, Name = "Sophia", Designation = "Developer", JoiningDate = DateTime.Now.AddMonths(-2), AddressId = 2, ImagePath = "" },
                  new User() { UserId = 6, Name = "Olivia", Designation = "HR", JoiningDate = DateTime.Now.AddYears(-3), AddressId = 3, ImagePath = "" },
                   new User() { UserId = 7, Name = "Liam", Designation = "Developer", JoiningDate = DateTime.Now.AddYears(-1), AddressId = 1, ImagePath = "" },
                 new User() { UserId = 8, Name = "Benjamin", Designation = "Developer", JoiningDate = DateTime.Now.AddDays(-2), AddressId = 2, ImagePath = "" },
                  new User() { UserId = 9, Name = "Elijah", Designation = "HR", JoiningDate = DateTime.Now.AddYears(-3), AddressId = 3, ImagePath = "" },
                  new User() { UserId = 10, Name = "John", Designation = "Developer", JoiningDate = DateTime.Now.AddMonths(-1), AddressId = 1, ImagePath = "" },
                 new User() { UserId = 11, Name = "Mark", Designation = "Developer", JoiningDate = DateTime.Now.AddMonths(-2), AddressId = 2, ImagePath = "" },
                  new User() { UserId = 12, Name = "Smith", Designation = "Developer", JoiningDate = DateTime.Now.AddMonths(-3), AddressId = 3, ImagePath = "" }
                  );

        }
    }
}
