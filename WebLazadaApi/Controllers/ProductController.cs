using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ShopLazadaContext _context;
        public ProductController(ShopLazadaContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Danh sách sản phẩm
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListProduct")]
        public IActionResult Get()
        {
            try
            {
                var products = _context.Products.ToList();
                if (products.Count == 0)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(products);
            }
            catch
            {
                return StatusCode(500, "An error has occurred");
            }
        }
        /// <summary>
        /// Thông tin sản phẩm 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public IActionResult GetbyId(int id)
        {
            try
            {
                var products = _context.Products.Include(m=>m.Category).Where(m => m.ProductId == id).FirstOrDefault();
                if (products == null)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(products);
            }
            catch
            {
                return StatusCode(500, "An error has occurred");
            }
        }
        /// <summary>
        /// Tạo mới sản phẩm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddProduct")]
        public IActionResult Create([FromBody] Product model)
        {
            Product product = new Product();
            product.ProductId = model.ProductId;
            product.ProductName = model.ProductName;
            product.Description = model.Description;
            product.Image = model.Image;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;

            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(500, " Error ");
            }
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpPut("UpdateProduct")]
        public IActionResult Update([FromBody] Product model, int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return StatusCode(404, "User not found");
                }
                product.ProductName = model.ProductName;
                product.Description = model.Description;
                product.Image = model.Image;
                product.Price = model.Price;
                product.CategoryId = model.CategoryId;
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return StatusCode(404, "User not found");
                }
                _context.Entry(product).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            var products = _context.Products.ToList();
            return Ok(products);
        }
    }
}

