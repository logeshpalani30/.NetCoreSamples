using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using EfDbModelDemo.Interface;
using EfDbModelDemo.Models;

namespace EfAdoTemplateTest.Repository
{
    public class ProductImagesRepository : IProductImages
    {
        private logesh_dbContext dbContext;
        public ProductImagesRepository(logesh_dbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ProductImages> GetAll()
        {
            try
            {
                var data= dbContext.ProductImages.Select(x=>x);
                dbContext.SaveChanges();
                return data.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public void SaveImage()
        {
            try
            {
                ProductImages image = new ProductImages()
                {
                    ImageName = DateTime.Now.ToLongDateString(),
                    ImageSource = GetFileBytes()
                };
                dbContext.ProductImages.Add(image);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                
            }
        }

        public  byte[] GetFileBytes()
        {
            try
            {
                byte[] buf = new byte[1024];
                int c;

                string exePath = @"C:\.NetCoreLearning\EfAdoTemplateTest\image.jpg";
                buf = System.IO.File.ReadAllBytes(exePath);

                return buf;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public byte[] GetImage(int Id)
        {
            try
            {
                var imageByte = dbContext.ProductImages.SingleOrDefault(x => x.ImageId == 4);
                return imageByte.ImageSource;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}