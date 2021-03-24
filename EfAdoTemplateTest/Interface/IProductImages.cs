using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfDbModelDemo.Models;

namespace EfDbModelDemo.Interface
{
    public interface IProductImages
    {
        void SaveImage();
        List<ProductImages> GetAll();
        byte[] GetImage(int Id);

    }
}
