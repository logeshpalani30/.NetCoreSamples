using System;
using System.Collections.Generic;

namespace UserDataFlow.Models
{
    public partial class UserAddress
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string DoorNo { get; set; }
        public string Street { get; set; }
        public string PinCode { get; set; }

        public virtual BaseAddress PinCodeNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
