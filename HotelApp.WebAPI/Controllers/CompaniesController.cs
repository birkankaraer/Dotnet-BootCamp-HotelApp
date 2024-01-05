using HotelApp.DataAccess;
using HotelApp.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly HotelAppDbContext _context;

        public CompaniesController()
        {
            _context = new HotelAppDbContext();
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = _context.Company.ToList();
            return Ok(companies);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var company = _context.Company.FirstOrDefault(x => x.Id == id);
            if (company == null) return NotFound();
            else return Ok(company);
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (company == null) return BadRequest("Geçersiz veri.");

            company.AddDate = DateTime.Now;
            _context.Company.Add(company);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created, company);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Company company)
        {
            if (company == null) return BadRequest();

            var existingCompany = _context.Company.Find(id);
            if (existingCompany == null)
            {
                return NotFound();
            }

            existingCompany.Name = company.Name;
            existingCompany.Address = company.Address;
            _context.Update(existingCompany);
            _context.SaveChanges();
            return Ok(existingCompany);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var company = _context.Company.FirstOrDefault(r => r.Id == id);

            if (company == null) return NotFound();
            else
            {
                _context.Company.Remove(company);
                _context.SaveChanges();
                return Ok(company);
            }
        }
    }
}
