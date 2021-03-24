using System;
using System.Collections.Generic;

namespace UserDataFlow.Models
{
    public partial class Department
    {
        public Department()
        {
            Role = new HashSet<Role>();
        }

        public int DepartmentId { get; set; }
        public string DepartName { get; set; }
        public string DepartDesc { get; set; }

        public virtual ICollection<Role> Role { get; set; }
    }
}
