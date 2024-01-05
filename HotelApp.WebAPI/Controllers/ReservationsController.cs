using HotelApp.DataAccess;
using HotelApp.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly HotelAppDbContext _context;

        public ReservationsController()
        {
            _context = new HotelAppDbContext();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var reservations = _context.Reservation.Include(x => x.Room).Include(x => x.Client.Company).ToList();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var reservation = _context.Reservation.Include(x => x.Room).Include(x => x.Client.Company).FirstOrDefault(r => r.Id == id);

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }


        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            if (reservation == null)
                return BadRequest("Invalid data");

            reservation.AddDate = DateTime.Now;
            Client client = _context.Client.Find(reservation.ClientId);
            Room room = _context.Room.Find(reservation.RoomId);
            if (room == null || client == null) return BadRequest();
            reservation.Room = room;
            reservation.RoomId = room.Id;
            reservation.Client = client;
            reservation.ClientId = client.Id;

            _context.Reservation.Add(reservation);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created, reservation);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Reservation reservation)
        {
            var existingReservation = _context.Reservation.FirstOrDefault(r => r.Id == id);

            if (existingReservation == null)
                return NotFound();

            Client client = _context.Client.Find(reservation.ClientId);
            Room room = _context.Room.Find(reservation.RoomId);
            if (room == null || client == null) return BadRequest();
            existingReservation.Room = room;
            existingReservation.RoomId = room.Id;
            existingReservation.Client = client;
            existingReservation.ClientId = client.Id;

            existingReservation.ReservationDate = reservation.ReservationDate;
            existingReservation.ReservationEndDate = reservation.ReservationEndDate;
            existingReservation.RoomId = reservation.RoomId;
            existingReservation.ClientId = reservation.ClientId;

            _context.Reservation.Update(existingReservation);
            _context.SaveChanges();

            return Ok(existingReservation);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservation = _context.Reservation.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
                return NotFound();

            _context.Reservation.Remove(reservation);
            _context.SaveChanges();

            return Ok(reservation);
        }
    }
}
