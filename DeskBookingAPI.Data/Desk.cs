using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DeskBookingAPI.Data
{
    public class Desk
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Number { get; set; }

        public bool IsStanding { get; set; }

        [Column("RoomId")]
        public int CompanyRoomId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public CompanyRoom? CompanyRoom { get; set; }

        [JsonIgnore]
        public List<Booking>? Bookings { get; set; }
    }
}
