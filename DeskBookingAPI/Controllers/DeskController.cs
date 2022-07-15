using DeskBookingAPI.Data;
using DeskBookingAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace DeskBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeskController : Controller
    {
        private readonly DeskBookingContext _deskBookingContext;

        public DeskController(DeskBookingContext deskBookingContext)
        {
            _deskBookingContext = deskBookingContext;
        }

        // GET: api/<DeskController>
        [HttpGet]
        public IEnumerable Get()
        {
            return _deskBookingContext.Desks.ToList();
        }


        [HttpGet("pretty")]
        public IEnumerable GetPretty()
        {
            #region with LINQ query
            //return from desk in _deskBookingContext.Desks
            //       join companyRoom in _deskBookingContext.CompanyRooms on desk.CompanyRoomId equals companyRoom.Id
            //       select new DeskDTO()
            //       {
            //           DeskNumber = desk.Number,
            //           IsStanding = (desk.IsStanding ? "yes" : "no"),
            //           RoomName = companyRoom.Name,
            //           RoomNumber = companyRoom.Number,
            //           RoomFloor = companyRoom.Floor

            //       };
            #endregion

            return _deskBookingContext.Desks.Include(d => d.CompanyRoom).Select(desk => new DeskDTO()
            {
                DeskNumber = desk.Number,
                IsStanding = desk.IsStanding ? "yes" : "no",
                RoomName = desk.CompanyRoom.Name,
                RoomNumber = desk.CompanyRoom.Number,
                RoomFloor = desk.CompanyRoom.Floor
            });
         }

        // GET api/<DeskController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var desk = _deskBookingContext.Desks.FirstOrDefault(t => t.Id == id);
            if (desk == null)
            {
                return NotFound();
            }
            return new ObjectResult(desk);
        }

        // POST api/<DeskController>
        [HttpPost]
        public IActionResult Create([FromBody] Desk desk)
        {
            if (desk == null)
            {
                return NotFound();
            }

            var companyRoom = _deskBookingContext.CompanyRooms.FirstOrDefault(t => t.Id == desk.CompanyRoomId);
            if (companyRoom == null)
            {
                return BadRequest();
            }
            desk.CompanyRoom = companyRoom;

            _deskBookingContext.Desks.Add(desk);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

        // PUT api/<DeskController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Desk desk)
        {
            if (desk == null || desk.Id != id)
            {
                return BadRequest();
            }

            var previousDesk = _deskBookingContext.Desks.FirstOrDefault(t => t.Id == id);
            if (previousDesk == null)
            {
                return NotFound();
            }

            var companyRoom = _deskBookingContext.CompanyRooms.FirstOrDefault(t => t.Id == desk.CompanyRoomId);
            if (companyRoom == null)
            {
                return BadRequest();
            }

            previousDesk.CompanyRoomId = companyRoom.Id;
            previousDesk.CompanyRoom = companyRoom;

            previousDesk.Number = desk.Number;
            previousDesk.IsStanding = desk.IsStanding;

            _deskBookingContext.Desks.Update(previousDesk);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}/moveTo/{roomId}")]
        public IActionResult MoveTo(int id, int roomId)
        {
            #region separate queries
            //var desk = _deskBookingContext.Desks.FirstOrDefault(t => t.Id == id);
            //if (desk == null)
            //{
            //    return NotFound(string.Format("The desk with id {0} doesn't exist", id));
            //}

            //var newRoom = _deskBookingContext.CompanyRooms.FirstOrDefault(t => t.Id == roomId);

            //if (newRoom == null)
            //{
            //    return NotFound(string.Format("The room with id {0} doesn't exist", roomId));
            //}

            //var oldRoom = _deskBookingContext.CompanyRooms.FirstOrDefault(t => t.Id == desk.CompanyRoomId);

            //if (newRoom.CompanyId != oldRoom.CompanyId)
            //{
            //    return BadRequest("Cannot move desk to another company");
            //}

            //desk.CompanyRoomId = roomId;

            //_deskBookingContext.Desks.Update(desk);
            //_deskBookingContext.SaveChanges();

            //return Ok();
            #endregion

            var desk = _deskBookingContext.Desks.Include(d=> d.CompanyRoom).FirstOrDefault(t => t.Id == id);
            if (desk == null)
            {
                return NotFound(string.Format("The desk with id {0} doesn't exist", id));
            }

            var newRoom = _deskBookingContext.CompanyRooms.FirstOrDefault(t => t.Id == roomId);

            if (newRoom == null)
            {
                return NotFound(string.Format("The room with id {0} doesn't exist", roomId));
            }

            if (newRoom.CompanyId != desk.CompanyRoom.CompanyId)
            {
                return BadRequest("Cannot move desk to another company");
            }

            desk.CompanyRoomId = roomId;
            desk.CompanyRoom = newRoom;

            _deskBookingContext.Desks.Update(desk);
            _deskBookingContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<DeskController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var desk = _deskBookingContext.Desks.FirstOrDefault(t => t.Id == id);
            if (desk == null)
            {
                return NotFound();
            }

            _deskBookingContext.Desks.Remove(desk);
            _deskBookingContext.SaveChanges();

            return Ok();
        }
    }
}
