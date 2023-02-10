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
    public class RoleController : ControllerBase
    {
        private ShopLazadaContext _context;
        public RoleController(ShopLazadaContext context)
        {
            _context = context;
        }

        [HttpGet("ListRole")]
        public IActionResult Get()
        {
            try
            {
                var users = _context.Roles.ToList();
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
                var users = _context.Roles.Where(m => m.RoleId == id);
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
        [HttpPost("AddRole")]
        public IActionResult Create([FromBody] Role model)
        {
            Role role = new Role();
            role.RoleId = model.RoleId;
            role.RoleName = model.RoleName;
            try
            {
                _context.Roles.Add(role);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(500, " Error ");
            }
            var roles = _context.Roles.ToList();
            return Ok(roles);
        }

        [HttpPut("UpdateRole")]
        public IActionResult Update([FromBody] Role model, int id)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(x => x.RoleId == id);
                if (role == null)
                {
                    return StatusCode(404, "User not found");
                }
                role.RoleName = model.RoleName;

                _context.Entry(role).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            var roles = _context.Roles.ToList();
            return Ok(roles);
        }

        [HttpDelete("DeleteRole")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(x => x.RoleId == id);
                if (role == null)
                {
                    return StatusCode(404, "User not found");
                }
                _context.Entry(role).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            var roles = _context.Roles.ToList();
            return Ok(roles);
        }
    }
}
