using System;
using System.Collections.Generic;

namespace EfDbModelDemo.Models
{
    public partial class ProductImages
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageSource { get; set; }
    }
}
