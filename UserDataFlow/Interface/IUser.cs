using System.Collections.Generic;
using UserDataFlow.Model;
using UserDataFlow.Model.User;
using UserDataFlow.Models;

namespace UserDataFlow.Interface
{
    public interface IUser
    {
        int AddUser(UserModel user);
        
        bool DeleteUser(int id);
        
        bool UpdateUser(int id, UserModel userModel);

        User GetUser(int id);

        List<dynamic> GetUsers();

        LoginResultModel Login(LoginModel user);
    }
}