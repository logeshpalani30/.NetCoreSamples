using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDwithASPdotNetCore.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }
        public string Description { get; set; }
        public int  Quality { get; set; }
    }
}
