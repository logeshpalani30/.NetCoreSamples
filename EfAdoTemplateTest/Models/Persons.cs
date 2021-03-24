using System;
using System.Collections.Generic;

namespace EfDbModelDemo.Models
{
    public partial class Persons
    {
        public Persons()
        {
            Orders = new HashSet<Orders>();
        }

        public int PersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? Age { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
