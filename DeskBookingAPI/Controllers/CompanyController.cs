using DeskBookingAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeskBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly DeskBookingContext _deskBookingContext;

        public CompanyController(DeskBookingContext deskBookingContext)
        {
            _deskBookingContext = deskBookingContext;
        }

        // GET: api/<CompanyController>
        [HttpGet]
        public IEnumerable Get()
        {
            return _deskBookingContext.Companies.ToList();
        }

        // GET api/<CompanyController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var company = _deskBookingContext.Companies.FirstOrDefault(t => t.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return new ObjectResult(company);
        }

        // POST api/<CompanyController>
        [HttpPost]
        public IActionResult Create([FromBody] Company company)
        {
            if (company == null)
            {
                return NotFound();
            }

            _deskBookingContext.Companies.Add(company);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

        // PUT api/<CompanyController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Company company)
        {
            if (company == null || company.Id != id)
            {
                return BadRequest();
            }

            var previousCompany = _deskBookingContext.Companies.FirstOrDefault(t => t.Id == id);
            if(previousCompany == null)
            {
                return NotFound();
            }

            previousCompany.Address = company.Address;
            previousCompany.Name = company.Name;

            _deskBookingContext.Companies.Update(previousCompany);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<CompanyController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var company = _deskBookingContext.Companies.FirstOrDefault(t => t.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            _deskBookingContext.Companies.Remove(company);
            _deskBookingContext.SaveChanges();

            return Ok();
        }
    }
}
