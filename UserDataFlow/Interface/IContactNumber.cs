using System.Collections.Generic;
using UserDataFlow.Model.Contact;

namespace UserDataFlow.Interface
{
    public interface IContactNumber
    {
        ContactNumberModel AddContact(AddContactModel contactModel);
        ContactNumberModel UpdateContact(ContactNumberModel userContact);
        string DeleteContact(int userId, int contactId);
        IEnumerable<ContactNumberModel> GetContacts(int id);
    }
}