using DeskBookingAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace DeskBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRoleController : Controller
    {

        private readonly DeskBookingContext _deskBookingContext;

        public EmployeeRoleController(DeskBookingContext deskBookingContext)
        {
            _deskBookingContext = deskBookingContext;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable Get()
        {
            return _deskBookingContext.EmployeeRoles.ToList();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employee = _deskBookingContext.EmployeeRoles.FirstOrDefault(t => t.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return new ObjectResult(employee);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EmployeeRole employeeRole)
        {
            if (employeeRole == null || employeeRole.Id != id)
            {
                return BadRequest();
            }

            var originalEmployeeRole = _deskBookingContext.EmployeeRoles.FirstOrDefault(t => t.Id == id);
            if (originalEmployeeRole == null)
            {
                return NotFound();
            }

            originalEmployeeRole.Name = employeeRole.Name ?? originalEmployeeRole.Name;

            _deskBookingContext.EmployeeRoles.Update(originalEmployeeRole);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employeeRole = _deskBookingContext.EmployeeRoles.FirstOrDefault(t => t.Id == id);
            if (employeeRole == null)
            {
                return NotFound();
            }

            _deskBookingContext.EmployeeRoles.Remove(employeeRole);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

    }
}
