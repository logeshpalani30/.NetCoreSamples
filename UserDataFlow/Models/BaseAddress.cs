using System;
using System.Collections.Generic;

namespace UserDataFlow.Models
{
    public partial class BaseAddress
    {
        public BaseAddress()
        {
            UserAddress = new HashSet<UserAddress>();
        }

        public string PinCode { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Nationality { get; set; }

        public virtual ICollection<UserAddress> UserAddress { get; set; }
    }
}
