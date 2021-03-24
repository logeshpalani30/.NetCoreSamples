using EfAdoTemplateTest.Interface;
using EfDbModelDemo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EfAdoTemplateTest.Model;

namespace EfAdoTemplateTest.Repository
{
    public class EventsRepository : IEvents
    {
        private readonly logesh_dbContext context;

        public EventsRepository(logesh_dbContext context)
        {
            this.context = context;
        }
        public Organizer GetOrganizer(string organizer)
        {
            try
            {
                // var organizerData = context.Organizers.SingleOrDefault(c=>c.Organizer==organizer);
                // var eventData = context.Events.Where(c => c.Organizer == organizer).ToList(); 
                //
                // var OrganizerDetails = new Organizer()
                // {
                //     Experience = organizerData?.Experience,
                //     Owner = organizerData?.Organizer,
                //     Technology = organizerData?.Technology,
                //     SpeakingEvents = eventData??new List<Events>()
                // };
            
                // return OrganizerDetails;
                return null;
            }
            catch (Exception e)
            {
            }

            return null;
        }
        public  List<Events> GetAllEvents()
        {
            try
            {
                // var data = context.Events.ToList();
                // return data;
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool AddEvent(Events eEvent)
        {
            try
            {
               // var data = context.Events.Add(eEvent);
               // context.SaveChanges();
               // var id = data.Entity.EventId;
               // Debug.Print(id.ToString());
               //
               return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Events GetEvent(int id)
        {
            try
            {
                // var data = context.Events.SingleOrDefault(x => x.EventId == id);
                // return data;
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool DeleteEvent(int id)
        {
            try
            {
                // var deleteEvent= context.Events.SingleOrDefault(x => x.EventId == id);
                // context.Events.Remove(deleteEvent);
                // context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public string UpdateEvent(int id, Events updatedEvent)
        {
            // try
            // {
            //     var updateEvent = context.Events.SingleOrDefault(c=>c.EventId==id);
            //     
            //     if (updateEvent !=null)
            //     {
            //         updateEvent.AttendeeCount= updatedEvent.AttendeeCount;
            //         updateEvent.Organizer= updatedEvent.Organizer;
            //         updateEvent.DateOfEvent= updatedEvent.DateOfEvent;
            //         updateEvent.DateOfPublish= updatedEvent.DateOfPublish;
            //         updateEvent.EventType= updatedEvent.EventType;
            //         updateEvent.LocationOfEvent= updatedEvent.LocationOfEvent;
            //         updateEvent.Subject= updatedEvent.Subject;
            //         updateEvent.Technology = updatedEvent.Technology;
            //         updateEvent.Title = updatedEvent.Title;
            //
            //         context.SaveChanges();
            //         return "Updated";
            //     }
            //
            //     return "Event not Exist";
            // }
            // catch (Exception e)
            // {
            //     return "Event not exist";
            // }

            return "Event not exist";
        }
    }
}