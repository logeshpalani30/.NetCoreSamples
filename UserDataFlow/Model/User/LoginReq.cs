using System.Collections.Generic;
using UserDataFlow.Model.Address;
using UserDataFlow.Model.Contact;

namespace UserDataFlow.Model.User
{
    public class LoginReq
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
    // public class LoginResultModel
    // {
    //     public int UserId { get; set; }
    //     public string FirstName { get; set; }
    //     public string LastName { get; set; }
    //     public string Gender { get; set; }
    //     public string? PhotoUrl { get; set; }
    //     public string? Email { get; set; }
    //     public IEnumerable<ContactNumberModel> Contacts { get; set; }
    // }
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
    }
    public class UserBasic: User
    { 
        public IEnumerable<AddContact> Contacts { get; set; }
        public IEnumerable<Address.Address> Addresses { get; set; }
    }

    public class UserDetail : UserBasic
    {
        public int UserId { get; set; }
    }
    public class UserSignup : UserBasic
    {
        public string Password { get; set; }
    }

    public class AddressAndContact : User
    {
        public int UserId { get; set; }
        public List<AddressUpdate> Addresses { get; set; }
        public List<ContactUpdate> Contacts { get; set; }
    }
}