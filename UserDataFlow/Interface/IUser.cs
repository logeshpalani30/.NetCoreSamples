using System.Collections.Generic;
using UserDataFlow.Model;
using UserDataFlow.Model.User;
using UserDataFlow.Models;

namespace UserDataFlow.Interface
{
    public interface IUser
    {
        AddressAndContact AddUser(UserSignup user);
        void DeleteUser(int id);
        UserDetail UpdateUser(UserDetail userModel);
        UserDetail GetUser(int id);
        IEnumerable<UserDetail> GetUsers();
        UserDetail Login(LoginReq user);
        void ResetPassword(LoginReq req);
    }
}