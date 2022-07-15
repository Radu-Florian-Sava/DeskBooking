using DeskBookingAPI.Data;
using DeskBookingAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace DeskBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyRoomController : Controller
    {
        private readonly DeskBookingContext _deskBookingContext;

        public CompanyRoomController(DeskBookingContext deskBookingContext)
        {
            _deskBookingContext = deskBookingContext;
        }

        // GET: api/<CompanyRoomController>
        [HttpGet]
        public IEnumerable Get()
        {
            return _deskBookingContext.CompanyRooms.ToList();
        }

        // GET api/<CompanyRoomController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var companyRoom = _deskBookingContext.CompanyRooms.FirstOrDefault(t => t.Id == id);
            if (companyRoom == null)
            {
                return NotFound();
            }
            return new ObjectResult(companyRoom);
        }

        // GET api/<CompanyRoomController>/byFloor/5
        [HttpGet("byFloor&Company")]
        public IEnumerable GetByFloor([FromQuery] string floor, [FromQuery] int companyId)
        {
            #region LINQ query
            //return from companyRoom in _deskBookingContext.CompanyRooms
            //      where companyRoom.Floor == floor && companyRoom.CompanyId == companyId
            //      select companyRoom;
            #endregion

            return _deskBookingContext.CompanyRooms.Where(r => r.Floor == floor && r.CompanyId == companyId);
        }

        // POST api/<CompanyRoomController>
        [HttpPost]
        public IActionResult Create([FromBody] CompanyRoom companyRoom)
        {
            if (companyRoom == null)
            {
                return NotFound();
            }

            var company = _deskBookingContext.Companies.FirstOrDefault(t => t.Id == companyRoom.CompanyId);
            if (company == null)
            {
                return BadRequest();
            }
            companyRoom.Company = company;

            _deskBookingContext.CompanyRooms.Add(companyRoom);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

        // PUT api/<CompanyRoomController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CompanyRoomDTO companyRoomDTO)
        {
            if (companyRoomDTO == null || companyRoomDTO.Id != id)
            {
                return BadRequest();
            }

            var companyRoom = _deskBookingContext.CompanyRooms.FirstOrDefault(t => t.Id == id);
            if (companyRoom == null)
            {
                return NotFound();
            }

            if (companyRoomDTO.CompanyId != null)
            {
                var company = _deskBookingContext.Companies.FirstOrDefault(t => t.Id == companyRoomDTO.CompanyId);
                if (company == null)
                {
                    return BadRequest();
                }

                companyRoom.CompanyId = (int)companyRoomDTO.CompanyId;
                companyRoom.Company = company;
            }

            companyRoom.Name = companyRoomDTO.Name ?? companyRoom.Name;
            companyRoom.Number = companyRoomDTO.Number ?? companyRoom.Number;
            companyRoom.Floor = companyRoomDTO.Floor ?? companyRoom.Floor;


            _deskBookingContext.CompanyRooms.Update(companyRoom);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<CompanyRoomController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var companyRoom = _deskBookingContext.CompanyRooms.FirstOrDefault(t => t.Id == id);
            if (companyRoom == null)
            {
                return NotFound();
            }

            _deskBookingContext.CompanyRooms.Remove(companyRoom);
            _deskBookingContext.SaveChanges();

            return Ok();
        }
    }
}
