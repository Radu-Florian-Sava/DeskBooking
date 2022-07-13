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

        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<CompanyRoom> CompanyRooms { get; set; }

        public DbSet<Desk> Desks { get; set; }

        public DbSet<Booking> Bookings { get; set; }

    }
}
