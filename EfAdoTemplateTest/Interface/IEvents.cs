using System.Collections.Generic;
using System.Threading.Tasks;
using EfAdoTemplateTest.Model;
using EfDbModelDemo.Models;

namespace EfAdoTemplateTest.Interface
{
    public interface IEvents
    {
        List<Events> GetAllEvents();
        bool AddEvent(Events eEvent);
        Events GetEvent(int id);
        bool DeleteEvent(int id);
        string UpdateEvent(int id, Events updatedEvent);
        Organizer GetOrganizer(string organizer);
    }
}