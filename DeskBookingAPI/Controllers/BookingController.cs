using DeskBookingAPI.Data;
using DeskBookingAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeskBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly DeskBookingContext _deskBookingContext;

        public BookingController(DeskBookingContext deskBookingContext)
        {
            _deskBookingContext = deskBookingContext;
        }

        // GET: api/<BookingController>
        [HttpGet]
        public IEnumerable Get()
        {
            return _deskBookingContext.Bookings.ToList();
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var company = _deskBookingContext.Bookings.FirstOrDefault(t => t.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return new ObjectResult(company);
        }

        // GET api/<BookingController>/byEmployee/5
        [HttpGet("byEmployee/{employeeId}")]
        public IEnumerable GetByEmployee(int employeeId, [FromQuery] bool excludePastDue = false)
        {
            if (excludePastDue)
            {
                return _deskBookingContext.Bookings.Where(b => b.EmployeeId == employeeId && (DateTime.Compare(b.Date,DateTime.UtcNow) >= 0));
            }

            return _deskBookingContext.Bookings.Where(b => b.EmployeeId == employeeId);
        }



        // POST api/<BookingController>
        [HttpPost]
        public IActionResult Create([FromBody] Booking booking)
        {
            if (booking == null)
            {
                return NotFound();
            }

            var desk = _deskBookingContext.Desks.Include(d => d.CompanyRoom).FirstOrDefault(d => d.Id == booking.DeskId);

            if (desk == null)
            {
                return BadRequest(string.Format("The desk with id {0} doesn't exist", booking.DeskId));
            }

            var employee = _deskBookingContext.Employees.FirstOrDefault(t => t.Id == booking.EmployeeId);

            if (employee == null)
            {
                return BadRequest(string.Format("The employee with id {0} doesn't exist", booking.EmployeeId));

            }

            if (desk.CompanyRoom.CompanyId != employee.CompanyId)
            {
                return BadRequest("You cannot book a desk at another company");
            }

            booking.Employee = employee;
            booking.Desk = desk;

            _deskBookingContext.Bookings.Add(booking);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var booking = _deskBookingContext.Bookings.FirstOrDefault(t => t.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            _deskBookingContext.Bookings.Remove(booking);
            _deskBookingContext.SaveChanges();

            return Ok();
        }
    }
}
