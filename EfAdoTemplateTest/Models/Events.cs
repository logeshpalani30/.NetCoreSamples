using System;
using System.Collections.Generic;

namespace EfDbModelDemo.Models
{
    public partial class Events
    {
        public DateTime DateOfPublish { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string Subject { get; set; }
        public bool EventType { get; set; }
        public int? AttendeeCount { get; set; }
        public string LocationOfEvent { get; set; }
        public string Technology { get; set; }
        public string Organizer { get; set; }
        public string Title { get; set; }
        public int EventId { get; set; }
    }
}
