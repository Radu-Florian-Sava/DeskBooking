using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBookingAPI.Data
{
    public class DeskBookingContext : DbContext
    {
        public DeskBookingContext(DbContextOptions options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeRole>().HasData(
                new EmployeeRole { Id = 1, Name = "Manager"},
                new EmployeeRole { Id = 2, Name = "Assistant" },
                new EmployeeRole { Id = 3, Name = "Recruiter" },
                new EmployeeRole { Id = 4, Name = "Developer" },
                new EmployeeRole { Id = 5, Name = "Tester" },
                new EmployeeRole { Id = 6, Name = "Intern" }
                );
        }

        public DbSet<EmployeeRole> EmployeeRoles { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<CompanyRoom> CompanyRooms { get; set; }

        public DbSet<Desk> Desks { get; set; }

        public DbSet<Booking> Bookings { get; set; }

    }
}
