using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfDbModelDemo.Interface;
using EfDbModelDemo.Models;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EfDbModelDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly ILogger _logger;
        public readonly IProductImages ProductImages;

        public ProductImageController(ILogger<ProductImageController> logger, IProductImages productImages)
        {
            _logger = logger;
            ProductImages = productImages;
        }
        // GET: api/<ProductImageController>
        [HttpGet]
        [Route("getAll")]
        public List<ProductImages> Get()
        {
            var data = ProductImages.GetAll();
            return data;
        }

        // GET api/<ProductImageController>/5
        [HttpGet("{id:int}")]
        [Route("getImage")]
        public IActionResult Get(int id)
        {

            var b = ProductImages.GetImage(id);
            return Ok(Convert.ToBase64String(b, 0, b.Length));
        }

        // POST api/<ProductImageController>
        [HttpPost]
        [Route("uploadImage")]
        public IActionResult Post()
        {
            try
            {
                ProductImages.SaveImage();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                return BadRequest();
            }
        }

        // PUT api/<ProductImageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductImageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
