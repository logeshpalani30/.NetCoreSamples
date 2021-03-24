using System;
using System.Collections.Generic;

namespace EfDbModelDemo.Models
{
    public partial class Organizers
    {
        public int OrganizersId { get; set; }
        public string Organizer { get; set; }
        public int? Experience { get; set; }
        public string Technology { get; set; }
    }
}
