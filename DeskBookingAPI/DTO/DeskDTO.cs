namespace DeskBookingAPI.DTO
{
    public class DeskDTO
    {
        public DeskDTO(){
        }

        public string DeskNumber { get; set; }
        public string IsStanding { get; set; }

        public string RoomName { get; set; }

        public string RoomNumber { get; set; }

        public string RoomFloor { get; set; }
    }
}
