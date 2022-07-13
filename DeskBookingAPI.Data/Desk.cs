using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskBookingAPI.Data
{
    public class Desk
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public bool IsStanding { get; set; }

        [Column("RoomId")]
        public int CompanyRoomId { get; set; }
        public CompanyRoom CompanyRoom { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
