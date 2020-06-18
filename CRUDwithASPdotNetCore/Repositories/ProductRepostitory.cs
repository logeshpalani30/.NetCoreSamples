using CRUDwithASPdotNetCore.Interfaces;
using CRUDwithASPdotNetCore.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace CRUDwithASPdotNetCore.Repositories
{
    public class ProductRepostitory : IProductRepostitory
    {
        private readonly IConfiguration _config;
        public ProductRepostitory(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MyDBConnectionString"));
            }
        }

        public Task<int> AddProduct(Product p)
        {
            using (var dbConnection = Connection)
            {
                var query = "Insert Into dbo.products (ProductName, Price, Description, Quality) values (@ProductName, @Price, @Description, @Quality)";

                dbConnection.Open();

                var result = dbConnection.Execute(query, new { ProductName = p.ProductName, Price = p.Price, Description = p.Description, Quality = p.Quality });

                Console.WriteLine(result);
                return Task.FromResult(result);
            }
        }

        public Task<int> DeleteProductById(int id)
        {
            using (var dbConnection = Connection)
            {
                var query = "DELETE FROM products where ProductId=@ProductId";

                dbConnection.Open();

                var result = dbConnection.Execute(query, new { ProductId = id });

                return Task.FromResult(result);
            }
        }

        public async Task<Product> GetProductByID(int id)
        {
            using (var dbConnection = Connection)
            {
                var query = "SELECT * From dbo.products Where ProductId=@ProductId";
                dbConnection.Open();
                var result = await dbConnection.QueryAsync<Product>(query, new { ProductId = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            using (var dbConnection = Connection)
            {
                var query = "SELECT * From dbo.products";
                dbConnection.Open();

                var result = await dbConnection.QueryAsync<Product>(query);
                return result.ToList<Product>();
            }
        }

        public Task<int> UpdateProductById(int id, Product p)
        {
            try
            {

                using (var dbConnection = Connection)
                {


                    var query = "UPDATE products SET ProductName = @ProductName,Price = @Price,Description = @Description,Quality = @Quality WHERE ProductId = @ProductId;";
                    dbConnection.Open();
                    //SqlCommand sqlcom = new SqlCommand(query, dbConnection);
                    var result = dbConnection.Execute(query, new { ProductId = id, ProductName = p.ProductName, Price = p.Price, Description = p.Description, Quality = p.Quality });

                    return Task.FromResult(result);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
