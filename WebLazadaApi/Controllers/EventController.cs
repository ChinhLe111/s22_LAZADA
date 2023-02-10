using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLazadaApi.Models;

namespace WebLazadaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private ShopLazadaContext _context;
        public EventController(ShopLazadaContext context)
        {
            _context = context;
        }

        [HttpGet("ListEvent")]
        public IActionResult Get()
        {
            try
            {
                var users = _context.Events.ToList();
                if (users.Count == 0)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(users);
            }
            catch
            {
                return StatusCode(500, "An error has occurred");
            }
        }
        [HttpGet("GetById")]
        public IActionResult GetbyId(int id)
        {
            try
            {
                var users = _context.Events.Where(m => m.EventId == id);
                if (users == null)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(users);
            }
            catch
            {
                return StatusCode(500, "An error has occurred");
            }
        }
        [HttpPost("AddEvent")]
        public IActionResult Create([FromBody] Event model)
        {
            Event eve = new Event();
            eve.EventId = model.EventId;
            eve.EventName = model.EventName;
            eve.Description = model.Description;
            eve.EndDay = model.EndDay;
            eve.StartDay = model.StartDay;
            eve.Image = model.Image;
            try
            {
                _context.Events.Add(eve);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(500, " Error ");
            }
            var eves = _context.Events.ToList();
            return Ok(eves);
        }

        [HttpPut("UpdateEvent")]
        public IActionResult Update([FromBody] Event model, int id)
        {
            try
            {
                var eve = _context.Events.FirstOrDefault(x => x.EventId == id);
                if (eve == null)
                {
                    return StatusCode(404, "User not found");
                }
                eve.EventName = model.EventName;
                eve.Description = model.Description;
                eve.EndDay = model.EndDay;
                eve.StartDay = model.StartDay;
                eve.Image = model.Image;

                _context.Entry(eve).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            var eves = _context.Events.ToList();
            return Ok(eves);
        }

        [HttpDelete("DeleteEvent")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var eve = _context.Events.FirstOrDefault(x => x.EventId == id);
                if (eve == null)
                {
                    return StatusCode(404, "User not found");
                }
                _context.Entry(eve).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            var eves = _context.Events.ToList();
            return Ok(eves);
        }
    }
}
