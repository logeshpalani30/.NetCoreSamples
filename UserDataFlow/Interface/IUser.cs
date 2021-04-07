using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UserDataFlow.Model;
using UserDataFlow.Model.User;
using UserDataFlow.Models;
using User = UserDataFlow.Models.User;

namespace UserDataFlow.Interface
{
    public interface IUser
    {
        AddressAndContact AddUser(UserSignup user);
        void DeleteUser(int id);
        UserDetail UpdateUser(UserDetail userModel);
        User GetUser(int id);
        Task<IQueryable<UserDetail>> GetUsers();
        UserDetail Login(LoginReq user);
        void ResetPassword(LoginReq req);

      string AddUsersFromExcel(IFormFile file);
    }
}