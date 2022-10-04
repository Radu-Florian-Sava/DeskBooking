using DeskBookingAPI.Data;
using DeskBookingAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace DeskBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {

        private readonly DeskBookingContext _deskBookingContext;

        public EmployeeController(DeskBookingContext deskBookingContext)
        {
            _deskBookingContext = deskBookingContext;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable Get()
        {
            return _deskBookingContext.Employees.ToList();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employee = _deskBookingContext.Employees.FirstOrDefault(t => t.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return new ObjectResult(employee);
        }

        // GET api/<EmployeeController>/5/favDesk
        [HttpGet("{empId}/favDesk")]
        public IActionResult GetFavDesk(int empId)
        {
            #region LINQ query
            //var favDesk = (from b in _deskBookingContext.Bookings
            //              where b.EmployeeId == empId
            //              group b by b.DeskId into gr
            //              orderby gr.Count() descending
            //              select new FavDeskDTO()
            //              {
            //                  DeskId = gr.Key,
            //                  NumberOfBookings = gr.Count()
            //              }).First();
            #endregion

            var favDesk = _deskBookingContext.Bookings.Where(b => b.EmployeeId == empId).
                GroupBy(b => b.DeskId).
                OrderBy(gr => gr.Count()).
                Select(gr => new FavDeskDTO()
                {
                    DeskId = gr.Key,
                    NumberOfBookings = gr.Count()
                }).
                Last();
            return new ObjectResult(favDesk);
        }


        // GET api/<EmployeeController>/byCompany/5
        [HttpGet("byCompany/{companyId}")]
        public IEnumerable GetByCompany(int companyId)
        {
            return from employee in _deskBookingContext.Employees 
                   where employee.CompanyId == companyId 
                   select employee;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return NotFound();
            }

            var company = _deskBookingContext.Companies.FirstOrDefault(t => t.Id == employee.CompanyId);
            if (company == null)
            {
                return BadRequest();
            }
            employee.Company = company;

            _deskBookingContext.Employees.Add(employee);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null || employeeDTO.Id != id)
            {
                return BadRequest();
            }

            var employee = _deskBookingContext.Employees.FirstOrDefault(t => t.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            
            if(employeeDTO.CompanyId != null)
            {
                var company = _deskBookingContext.Companies.FirstOrDefault(t => t.Id == employeeDTO.CompanyId);
                if (company == null)
                {
                    return BadRequest();
                }

                employee.CompanyId = (int) employeeDTO.CompanyId;
                employee.Company = company;
            }

            employee.Name = employeeDTO.Name ?? employee.Name;

            employee.Email = employeeDTO.Email??employee.Email;

            employee.EmployeeRoleId = employeeDTO.EmployeeRoleId ?? employee.EmployeeRoleId;

            _deskBookingContext.Employees.Update(employee);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employee = _deskBookingContext.Employees.FirstOrDefault(t => t.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            _deskBookingContext.Employees.Remove(employee);
            _deskBookingContext.SaveChanges();

            return Ok();
        }
    }
}
