using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLazadaApi.Models;

namespace WebLazadaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private ShopLazadaContext _context;
        public AccountController(ShopLazadaContext context)
        {
            _context = context;
        }

        [HttpGet("ListAccount")]
        public IActionResult Get()
        {
            try
            {
                var users = _context.AccountLogins.ToList();
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
                var users = _context.AccountLogins.Include(m=>m.Role).Where(m => m.UserId == id).FirstOrDefault();
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
        [HttpPost("AddAccount")]
        public IActionResult Create([FromBody] AccountLogin model)
        {
            AccountLogin acc = new AccountLogin();
                acc.UserId = model.UserId;
                acc.UserName = model.UserName;
                acc.Password = model.Password;
                acc.RoleId = model.RoleId;

                try
                {
                    _context.AccountLogins.Add(acc);
                    _context.SaveChanges();

                }
                catch (Exception ex)
                {
                    return StatusCode(500, " Error ");
                }
            var accs = _context.AccountLogins.ToList();
                return Ok(accs);
            }

        [HttpPut("UpdateAccount")]
        public IActionResult Update([FromBody] AccountLogin model,int id)
        {
            try 
            {
                var user = _context.AccountLogins.FirstOrDefault(x => x.UserId == id);
                if(user == null)
                {
                    return StatusCode(404, "User not found");
                }
                user.UserName = model.UserName;
                user.Password = model.Password;
                user.RoleId = model.RoleId;

                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            var accs = _context.AccountLogins.ToList();
            return Ok(accs); 
        }
        /// <summary>
        /// Xoá tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteAccount")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var user = _context.AccountLogins.FirstOrDefault(x => x.UserId == id);
                if (user == null)
                {
                    return StatusCode(404, "User not found");
                }
                _context.Entry(user).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            var accs = _context.AccountLogins.ToList();
            return Ok(accs);
        }

    }
}
