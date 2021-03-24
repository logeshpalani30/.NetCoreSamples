using System;
using System.Collections.Generic;

namespace UserDataFlow.Models
{
    public partial class User
    {
        public User()
        {
            UserAddress = new HashSet<UserAddress>();
            UserContact = new HashSet<UserContact>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<UserAddress> UserAddress { get; set; }
        public virtual ICollection<UserContact> UserContact { get; set; }
    }
}
