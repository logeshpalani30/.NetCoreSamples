using System;
using System.Collections.Generic;

namespace EfDbModelDemo.Models
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public int? PersonId { get; set; }

        public virtual Persons Person { get; set; }
    }
}
