using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UserDataFlow.Interface;
using UserDataFlow.Model.Address;
using UserDataFlow.Model.Contact;
using UserDataFlow.Model.User;
using UserDataFlow.Models;
using User = UserDataFlow.Models.User;

namespace UserDataFlow.Repository
{
    public class UserRepository : IUser
    {
        private readonly logesh_user_task_dbContext _dbContext;

        public UserRepository(logesh_user_task_dbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AddressAndContact AddUser(UserSignup user)
        {
            if (string.IsNullOrEmpty(user.FirstName.Trim()))
                throw new Exception("First name should not be empty");

            if (string.IsNullOrEmpty(user.LastName.Trim()))
                throw new Exception("Last name should not be empty");

            if (string.IsNullOrEmpty(user.Gender.Trim()))
                throw new Exception("Last name should not be empty");

            if (string.IsNullOrEmpty(user.Password.Trim()))
                throw new Exception("Last name should not be empty");

            if (user.RoleId == 0)
                throw new Exception("RoleID should not be empty");

            var userData = new Models.User()
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

            return new AddressAndContact()
            {
                UserId = userData.UserId,
                LastName = userData.LastName,
                FirstName = userData.FirstName,
                Contacts = AddContacts(userData.UserId, user),
                Email = userData.Email,
                RoleId = userData.RoleId ?? 0,
                Gender = userData.Gender,
                PhotoUrl = userData.PhotoUrl,
                Addresses = AddAddresses(userData.UserId, user)
            };
            
        }

        private List<AddressUpdate> AddAddresses(int userId, UserSignup user)
        {
            var addresses = new List<AddressUpdate>();

            if (user.Addresses.Any())
            {
                foreach (var a in user.Addresses)
                {
                    AddBaseAddress(a);

                    var addedAddress = _dbContext.UserAddress.Add(new UserAddress()
                    {
                        UserId = userId,
                        PinCode = a.PinCode,
                        DoorNo = a.DoorNo,
                        Street = a.Street
                    });

                    _dbContext.SaveChanges();

                    addresses.Add(new AddressUpdate()
                    {
                        AddressId = addedAddress.Entity.AddressId,
                        PinCode = a.PinCode,
                        DoorNo = a.DoorNo,
                        Street = a.Street,
                        City = a.City,
                        District = a.District,
                        Nationality = a.Nationality
                    });
                }
            }

            return addresses;
        }

        private void AddBaseAddress(Address a)
        {
            if (!_dbContext.BaseAddress.Any(c => c.PinCode == a.PinCode))
            {
                var baseAddress = new BaseAddress()
                {
                    City = a.City,
                    District = a.District,
                    Nationality = a.Nationality,
                    PinCode = a.Nationality
                };
                _dbContext.BaseAddress.Add(baseAddress);
                _dbContext.SaveChanges();
            }
        }

        private List<ContactUpdate> AddContacts(int userId, UserSignup user)
        {
            var addedContacts = new List<ContactUpdate>(); 
           
            if (user.Contacts.Any())
            {
                foreach (var contact in user.Contacts)
                {
                    var addedContact = _dbContext.UserContact.Add(new UserContact()
                    {
                        Number = contact.Number,
                        UserId = userId,
                        NumberType = contact.NumberType
                    });

                    _dbContext.SaveChanges();
                    
                    addedContacts.Add(new ContactUpdate()
                    {
                        Number = addedContact.Entity.Number,
                        ContactId = addedContact.Entity.ContactId,
                        NumberType = addedContact.Entity.NumberType
                    });
                }
            }

            return addedContacts;
        }

        public User GetUser(int id)
        {
            var user =_dbContext.User.SingleOrDefault(c => c.UserId == id);

            return user == null
                ? throw new Exception("User not found")
                : user;
        }

        public async Task<IQueryable<UserDetail>> GetUsers()
        {
            if (_dbContext.User.Any())
            {
                IQueryable<User> query = _dbContext.User;

                foreach (var user in query)
                {
                    user.UserContact = _dbContext.UserContact.Where(c => c.UserId == user.UserId).ToList();
                    user.UserAddress = _dbContext.UserAddress.Where(c => c.UserId == user.UserId).ToList();
                }

                // return await query.ToArrayAsync();


                return from u in _dbContext.User
                    select new UserDetail()
                    {
                        Email = u.Email,
                        FirstName = u.FirstName,
                        Gender = u.Gender,
                        LastName = u.LastName,
                        PhotoUrl = u.PhotoUrl,
                        RoleId = u.RoleId ?? 0,
                        UserId = u.UserId,
                        Contacts = from contact in _dbContext.UserContact
                            where contact.UserId == u.UserId
                            select new AddContact()
                            {
                                Number = contact.Number,
                                NumberType = contact.NumberType
                            },
                        Addresses = from userAddress in _dbContext.UserAddress
                            where userAddress.UserId == u.UserId
                            join baseAddress in _dbContext.BaseAddress on userAddress.PinCode equals baseAddress.PinCode
                            select new Address()
                            {
                                DoorNo = userAddress.DoorNo,
                                PinCode = userAddress.PinCode,
                                City = baseAddress.City,
                                District = baseAddress.District,
                                Nationality = baseAddress.Nationality,
                                Street = userAddress.Street
                            }
                    };
            }

            throw new Exception("No Users");
        }

        public UserDetail Login(LoginReq user)
        {
             if (user.UserId != 0 && !string.IsNullOrEmpty(user.Password.Trim()))
             {
                 var loggedUser = _dbContext.User.FirstOrDefault(c => c.UserId == user.UserId);

                 if (loggedUser == null) throw new Exception("User not found");
                 if (loggedUser.Password != user.Password) throw new Exception("Password wrong");

                 return new UserDetail()
                 {
                     UserId = loggedUser.UserId,
                     LastName = loggedUser.LastName,
                     Email = loggedUser.Email,
                     FirstName = loggedUser.FirstName,
                     Gender = loggedUser.Gender,
                     PhotoUrl = loggedUser.PhotoUrl,
                     Addresses = GetAddresses(loggedUser.UserId),
                     Contacts = GetContacts(loggedUser.UserId),
                     RoleId = loggedUser.RoleId ?? 0
                 };
             }

             throw new Exception("User id and password not empty");
        }

        public void ResetPassword(LoginReq req)
        {
            var user =_dbContext.User.SingleOrDefault(c => c.UserId == req.UserId);
            if (user == null) throw new Exception("User not exist");
            if (string.IsNullOrEmpty(req.Password.Trim()))
                throw new Exception("Password cannot be empty");
            
            user.Password = req.Password;
            _dbContext.User.Update(user);
            _dbContext.SaveChanges();
        }

        public string AddUsersFromExcel(IFormFile file)
        {
            IExcelDataReader reader = null;
            DataSet dataSet = new DataSet();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var path = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            reader = Path.GetExtension(path) switch
            {
                ".xls" => //Excel 97-03.
                    ExcelReaderFactory.CreateBinaryReader(file.OpenReadStream()),
                ".xlsx" => //Excel 07 and above.
                    ExcelReaderFactory.CreateOpenXmlReader(file.OpenReadStream()),
                _ => throw new Exception("File format wrong")
            };

            dataSet = reader.AsDataSet();
            reader?.Close();

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                foreach (DataTable dataTable in dataSet.Tables)
                {
                    for (var i = 1; i < dataTable.Rows.Count; i++)
                    {
                        var user = new User
                        {
                            FirstName = Convert.ToString(dataTable.Rows[i][0]),
                            LastName = Convert.ToString(dataTable.Rows[i][1]),
                            Gender = Convert.ToString(dataTable.Rows[i][2]),
                            PhotoUrl = Convert.ToString(dataTable.Rows[i][3]),
                            Email = Convert.ToString(dataTable.Rows[i][4]),
                            Password = Convert.ToString(dataTable.Rows[i][5]),
                        };
                        _dbContext.User.Add(user);
                    }
                }
            }
            else
                throw new Exception("Excel sheet empty");

            var changes = _dbContext.SaveChanges();

            if (changes>0)
                return "Added Successfully";

            throw new Exception("Internal server error");
        }

        public void DeleteUser(int id)
        {
            var user = _dbContext.User.SingleOrDefault(c => c.UserId == id);

            if (user == null) throw new Exception("User not exist");

            RemoveAddresses(user.UserId);

            RemoveContacts(user.UserId);

            _dbContext.Remove(user);
            _dbContext.SaveChanges();
        }

        private IEnumerable<Address> GetAddresses(int userId)
        {
            return from a in _dbContext.UserAddress
                where a.UserId == userId
                join b in _dbContext.BaseAddress on a.PinCode equals b.PinCode
                select new Address()
                {
                    DoorNo = a.DoorNo,
                    PinCode = a.PinCode,
                    City = b.City,
                    District = b.District,
                    Nationality = b.Nationality,
                    Street = a.Street
                };
        }

        private IEnumerable<AddContact> GetContacts(int userId)
        {
            return from contact in _dbContext.UserContact
                where contact.UserId == userId
                select new AddContact()
                {
                    Number = contact.Number,
                    NumberType = contact.NumberType
                };
        }

        private void RemoveContacts(int userId)
        {
            var contacts = _dbContext.UserContact.Where(c => c.UserId == userId);
            
            if (!contacts.Any()) return;

            _dbContext.UserContact.RemoveRange(contacts);
            _dbContext.SaveChanges();
        }

        private void RemoveAddresses(int userId)
        {
            var addresses = _dbContext.UserAddress.Where(c => c.UserId == userId);
            
            if (!addresses.Any()) return;

            _dbContext.UserAddress.RemoveRange(addresses);
            _dbContext.SaveChanges();
        }

        public UserDetail UpdateUser(UserDetail userModel)
        {
            var user = _dbContext.User.SingleOrDefault(c => c.UserId == userModel.UserId);

            if (user == null) throw new Exception("User not found");
            
            if (string.IsNullOrEmpty(user.FirstName.Trim()))
                throw new Exception("First name should not be empty");
            if (string.IsNullOrEmpty(user.LastName.Trim()))
                throw new Exception("Last name should not be empty");
            if (string.IsNullOrEmpty(user.Gender.Trim()))
                throw new Exception("Last name should not be empty");
            if (string.IsNullOrEmpty(user.Password.Trim()))
                throw new Exception("Last name should not be empty");
            if (user.RoleId == 0)
                throw new Exception("RoleID should not be empty");

            user.Email = userModel.Email;
            user.FirstName = userModel.FirstName;
            user.Gender = userModel.Gender;
            user.LastName = userModel.LastName;
            user.PhotoUrl = userModel.PhotoUrl;
            user.RoleId = userModel.RoleId;
            _dbContext.Update(user);
            _dbContext.SaveChanges();

            return userModel;
        }
    }
}