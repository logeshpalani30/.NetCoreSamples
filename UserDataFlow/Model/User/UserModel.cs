using System.Collections.Generic;

namespace UserDataFlow.Model.User
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public List<AddContact> Contacts { get; set; }
    }
}