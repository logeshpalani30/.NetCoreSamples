using System;
using System.Collections.Generic;

namespace UserDataFlow.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
