using HotelApp.DataAccess;
using HotelApp.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly HotelAppDbContext _context;

        public RoomsController()
        {
            _context = new HotelAppDbContext();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms = _context.Room.ToList();
            return Ok(rooms);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = _context.Room.FirstOrDefault(x => x.Id == id);
            if (room == null) return NotFound();
            else return Ok(room);
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (room == null) return BadRequest("Geçersiz veri.");

            room.AddDate = DateTime.Now;
            _context.Room.Add(room);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created, room);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Room room)
        {
            if (room == null) return BadRequest();

            var existingRoom = _context.Room.Find(id);
            if (existingRoom == null)
            {
                return NotFound();
            }

            existingRoom.Name = room.Name;
            _context.Update(existingRoom);
            _context.SaveChanges();
            return Ok(existingRoom);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = _context.Room.FirstOrDefault(r => r.Id == id);

            if (room == null) return NotFound();
            else
            {
                _context.Room.Remove(room);
                _context.SaveChanges();
                return Ok(room);
            }
        }
    }
}
