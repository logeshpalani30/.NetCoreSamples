using CRUDwithASPdotNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDwithASPdotNetCore.Interfaces
{
    public interface IProductRepostitory
    {
        Task<int> AddProduct(Product product);
        Task<List<Product>> GetProducts();
        Task<Product> GetProductByID(int id);
        Task<int> UpdateProductById(int id, Product p);
        Task<int> DeleteProductById(int id);
     }
}
