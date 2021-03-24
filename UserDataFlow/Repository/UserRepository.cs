using System;
using System.Collections.Generic;
using System.Linq;
using UserDataFlow.Interface;
using UserDataFlow.Model.Contact;
using UserDataFlow.Model.User;
using UserDataFlow.Models;

namespace UserDataFlow.Repository
{
    public class UserRepository : IUser
    {
        private readonly logesh_user_task_dbContext _dbContext;

        public UserRepository(logesh_user_task_dbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddUser(UserModel user)
        {
            if (string.IsNullOrEmpty(user.FirstName.Trim()))
            {
                throw new Exception("First name should not be empty");
            }
            else if(string.IsNullOrEmpty(user.LastName.Trim()))
            {
                throw new Exception("Last name should not be empty");
            }
            else if (string.IsNullOrEmpty(user.Gender.Trim()))
            {
                throw new Exception("Last name should not be empty");
            }
            if (string.IsNullOrEmpty(user.Password.Trim()))
            {
                throw new Exception("Last name should not be empty");
            }
            else
            {
                var userData = new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Gender = user.Gender,
                    Password = user.Password,
                    PhotoUrl = user.PhotoUrl
                };

                _dbContext.User.Add(userData);
                _dbContext.SaveChanges();
                return userData.UserId;
            }
        }

        public User GetUser(int id)
        {
            try
            {
                var user =_dbContext.User.SingleOrDefault(c => c.UserId == id);
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            throw new IndexOutOfRangeException();
        }

        public List<dynamic> GetUsers()
        {
            try
            {
                var users = _dbContext.User.Select(c=>c).ToList<dynamic>();
                if (users.Any())
                {
                    return users;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            throw new ApplicationException();
        }

        public LoginResultModel Login(LoginModel user)
        {
             if (user.UserId != 0 && !string.IsNullOrEmpty(user.Password.Trim()))
             {
                 var loggedUser = _dbContext.User.FirstOrDefault(c => c.UserId == user.UserId);

                 if (loggedUser != null)
                 {
                     if (loggedUser.Password != user.Password)
                     {
                         throw new Exception("Password wrong");
                     }
                     else
                     {
                         var userDetails = new LoginResultModel()
                         {
                             UserId = loggedUser.UserId,
                             LastName = loggedUser.LastName,
                             Email = loggedUser.Email,
                             FirstName = loggedUser.FirstName,
                             Gender = loggedUser.Gender,
                             PhotoUrl = loggedUser.PhotoUrl
                         };

                         loggedUser.Password = null;

                         var contacts = _dbContext.UserContact.Where(x => x.UserId == loggedUser.UserId).Select(c => new ContactNumberModel()
                         {
                             UserId = c.UserId,
                             ContactId = c.ContactId,
                             Number = c.Number,
                             NumberType = c.NumberType
                         });

                         userDetails.Contacts = contacts;

                         return userDetails;

                     }
                 }
             }
            
             throw new Exception("User not found");
        }

        public bool DeleteUser(int id)
        {
            var user = _dbContext.User.SingleOrDefault(c=>c.UserId == id);
            
            if (user!=null)
            {
                _dbContext.Remove(user);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }
        
        public bool UpdateUser(int id, UserModel userModel)
        {
            try
            {
                var user = _dbContext.User.SingleOrDefault(c => c.UserId == id);
                if (user !=null)
                {
                    user.Email = userModel.Email;
                    user.FirstName = userModel.FirstName;
                    user.Gender = userModel.Gender;
                    user.LastName = userModel.LastName;
                    user.PhotoUrl = userModel.PhotoUrl;

                    _dbContext.Update(user);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception a)
            {
                Console.WriteLine(a);
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}