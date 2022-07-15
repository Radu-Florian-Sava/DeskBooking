using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeskBookingAPI.Data
{
    public class Company
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public List<Employee>? Employees { get; set; }

        [JsonIgnore]
        public List<CompanyRoom>? CompanyRooms { get; set; }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
