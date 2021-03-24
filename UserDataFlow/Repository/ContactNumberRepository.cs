using System;
using System.Collections.Generic;
using System.Linq;
using UserDataFlow.Interface;
using UserDataFlow.Model.Contact;
using UserDataFlow.Models;
using Exception = System.Exception;

namespace UserDataFlow.Repository
{
    public class ContactNumberRepository : IContactNumber
    {
        private readonly logesh_user_task_dbContext _dbContext;

        public ContactNumberRepository(logesh_user_task_dbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ContactNumberModel AddContact(AddContactModel contactModel)
        {
            var userContact = new UserContact()
            {
                UserId = contactModel.UserId,
                Number = contactModel.Number,
                NumberType = contactModel.NumberType
            };

            var contact = _dbContext.UserContact.Add(userContact);
            _dbContext.SaveChanges();
           
            if (contact!=null)
            {
                return GetReturnableObject(contact.Entity);
            }

            throw new ArgumentException("Internal Server app");
        }

        private ContactNumberModel GetReturnableObject(UserContact contact)
        {
            return new ContactNumberModel()
            {
                ContactId = contact.ContactId,
                Number = contact.Number,
                NumberType = contact.NumberType,
                UserId = contact.UserId
            };
        }

        public ContactNumberModel UpdateContact(ContactNumberModel userContact)
        {
            var contact = _dbContext.UserContact.FirstOrDefault(c =>
                c.ContactId == userContact.ContactId && c.UserId == userContact.UserId);

            if (contact!=null)
            {
                contact.Number = userContact.Number;
                contact.NumberType = userContact.NumberType;

                _dbContext.Update(contact);
                _dbContext.SaveChanges();
                return GetReturnableObject(contact);
            }

            throw new Exception("User not found");
        }

        public string DeleteContact(int userId, int contactId)
        {
            var contact = _dbContext.UserContact.FirstOrDefault(c => c.UserId == userId && c.ContactId == contactId);

            if (contact!=null)
            {
                _dbContext.UserContact.Remove(contact);
                _dbContext.SaveChanges();
                return "Contact removed";
            }

            throw new Exception("Contact not exist");
        }

        public IEnumerable<ContactNumberModel> GetContacts(int id)
        {
           var contacts = (from contact in _dbContext.UserContact
                where contact.UserId == id
                select new ContactNumberModel()
                {
                    ContactId = contact.ContactId,
                    Number =  contact.Number,
                    NumberType = contact.NumberType,
                    UserId = contact.UserId
                }).ToList();

           if (contacts.Any()) return contacts;

           throw new Exception("No contacts");
        }
    }
}