using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDwithASPdotNetCore.Interfaces;
using CRUDwithASPdotNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDwithASPdotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepostitory _productRepostitory;

        public ProductController(IProductRepostitory productRepostitory)
        {
            _productRepostitory = productRepostitory;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<List<Product>> Get()
        {
            return  await _productRepostitory.GetProducts();
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Product> Get(int id)
        {
            return await _productRepostitory.GetProductByID(id);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<string> Post([FromBody] Product value)
        {
            var result = await _productRepostitory.AddProduct(value);

            return await Task.FromResult(result > 0 ? "Product Added" : "Internal Server Issue");
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<string> Put(int id, [FromBody] Product value)
        {
            var result = await _productRepostitory.UpdateProductById(id, value);
            return await Task.FromResult(result > 0 ? "Product Updated" : "No Records for update");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            var result = await _productRepostitory.DeleteProductById(id);
            return await Task.FromResult(result > 0 ? "Data Deleted Successfully" : "No Record Found for delete");
        }
    }
}
