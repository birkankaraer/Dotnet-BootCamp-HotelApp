namespace HotelApp.Entities.DTOs
{
    public class ReservationDetailDto
    {
        public DateTime AddDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public int RoomId { get; set; }
        public int ClientId { get; set; }
    }
}
