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
    }
}
