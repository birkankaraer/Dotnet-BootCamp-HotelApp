using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApp.Entities.Concrete
{
    public class Reservation : BaseModel
    {
        public DateTime ReservationDate { get; set; }
        public DateTime ReservationEndDate { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

    }
}
