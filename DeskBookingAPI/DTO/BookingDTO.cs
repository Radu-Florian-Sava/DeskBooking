namespace DeskBookingAPI.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? EmployeeId { get; set; }
        public int? DeskId { get; set; }
    }
}
