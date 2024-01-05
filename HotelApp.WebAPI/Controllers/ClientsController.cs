using HotelApp.DataAccess;
using HotelApp.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly HotelAppDbContext _context;
        public ClientsController()
        {
            _context = new HotelAppDbContext();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _context.Client.Include(x => x.Company).ToList();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = _context.Client.Include(x => x.Company).FirstOrDefault(x => x.Id == id);
            if (client == null) return NotFound();
            else return Ok(client);
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (client == null) return BadRequest("Geçersiz veri.");

            Company company = _context.Company.Find(client.CompanyId);
            if (company == null) return BadRequest("Şirket geçersiz.");

            client.AddDate = DateTime.Now;
            client.Company = company;
            _context.Client.Add(client);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created, client);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Client client)
        {
            if (client == null) return BadRequest();

            var existingClient = _context.Client.Find(id);
            if (existingClient == null)
            {
                return NotFound();
            }

            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.BirthDate = client.BirthDate;
            existingClient.Address = client.Address;
            existingClient.Email = client.Email;

            Company company = _context.Company.Find(client.CompanyId);
            if (company == null) return BadRequest("Şirket geçersiz.");
            existingClient.CompanyId = company.Id;
            existingClient.Company = company;

            _context.Update(existingClient);
            _context.SaveChanges();
            return Ok(existingClient);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var client = _context.Client.FirstOrDefault(c => c.Id == id);

            if (client == null) return NotFound();
            else
            {
                _context.Client.Remove(client);
                _context.SaveChanges();
                return Ok(client);
            }
        }

    }
}
