using System;
using System.Collections.Generic;

namespace UserDataFlow.Models
{
    public partial class UserContact
    {
        public int UserId { get; set; }
        public int ContactId { get; set; }
        public string Number { get; set; }
        public string NumberType { get; set; }

        public virtual User User { get; set; }
    }
}
