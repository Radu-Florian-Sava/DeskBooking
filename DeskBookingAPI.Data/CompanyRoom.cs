using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeskBookingAPI.Data
{
    public class CompanyRoom
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Floor { get; set; }
        public string Number { get; set; }
        public int CompanyId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Company? Company { get; set; }

        [JsonIgnore]
        public List<Desk>? Desks { get; set; } 
    }
}
