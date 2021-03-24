using System.Collections.Generic;
using EfDbModelDemo.Models;

namespace EfAdoTemplateTest.Model
{
    public class Organizer
    {
        public string Owner { get; set; }
        public int? Experience { get; set; }
        public string Technology { get; set; }

        public IList<Events> SpeakingEvents { get; set; }
    }
}