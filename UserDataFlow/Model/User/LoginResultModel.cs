using System.Collections.Generic;
using UserDataFlow.Model.Contact;

namespace UserDataFlow.Model.User
{
    public class LoginResultModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Email { get; set; }
        public IEnumerable<ContactNumberModel> Contacts { get; set; }
    }
}