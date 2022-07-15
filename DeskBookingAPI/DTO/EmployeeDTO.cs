using System.Text.Json.Serialization;

namespace DeskBookingAPI.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public int? CompanyId { get; set; }
    }
}
