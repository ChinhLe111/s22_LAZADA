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
    public class CategoryController : ControllerBase
    {
        private ShopLazadaContext _context;
        public CategoryController(ShopLazadaContext context)
        {
            _context = context;
        }

        [HttpGet("ListCategory")]
        public IActionResult Get()
        {
            try
            {
                var users = _context.Categories.ToList();
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
                var users = _context.Categories.Where(m => m.CategoryId == id);
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
        [HttpPost("AddCategory")]
        public IActionResult Create([FromBody] Category model)
        {
            Category cat = new Category();
            cat.CategoryId = model.CategoryId;
            cat.CategoryName = model.CategoryName;

            try
            {
                _context.Categories.Add(cat);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(500, " Error ");
            }
            var cats = _context.Categories.ToList();
            return Ok(cats);
        }

        [HttpPut("UpdateCategory")]
        public IActionResult Update([FromBody] Category model, int id)
        {
            try
            {
                var cat = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
                if (cat == null)
                {
                    return StatusCode(404, "User not found");
                }
                cat.CategoryName = model.CategoryName;

                _context.Entry(cat).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            var cats = _context.Categories.ToList();
            return Ok(cats);
        }

        [HttpDelete("DeleteCategory")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var cat = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
                if (cat == null)
                {
                    return StatusCode(404, "User not found");
                }
                _context.Entry(cat).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            var cats = _context.Categories.ToList();
            return Ok(cats);
        }
    }
}
