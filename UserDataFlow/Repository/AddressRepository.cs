using System;
using System.Collections.Generic;
using System.Linq;
using UserDataFlow.Interface;
using UserDataFlow.Model.Address;
using UserDataFlow.Models;

namespace UserDataFlow.Repository
{
    public class AddressRepository : IAddress
    {
        private readonly logesh_user_task_dbContext _dbContext;

        public AddressRepository(logesh_user_task_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AddressRes> GetAddress(int userId)
        {
            var address = (from a in _dbContext.UserAddress
                                        from b in _dbContext.BaseAddress
                                        where b.PinCode == a.PinCode
                                        where a.UserId == userId
                                        select new AddressRes()
                                        {
                                            UserId = a.UserId,
                                            DoorNo = a.DoorNo,
                                            PinCode = a.PinCode,
                                            AddressId = a.AddressId,
                                            City = b.City,
                                            District = b.District,
                                            Nationality = b.Nationality,
                                            Street = a.Street
                                        }).ToList();

            if (address.Any())
            {
                return address;
            }

            throw new Exception("Address not exist");
        }

        public AddressRes GetAddress(int userId, int addressId)
        {
            var differAddress = _dbContext.UserAddress.FirstOrDefault(c => c.UserId == userId && c.AddressId == addressId);
            var commonAddress = _dbContext.BaseAddress.FirstOrDefault(c => c.PinCode == differAddress.PinCode);

            if (differAddress!=null && commonAddress!=null)
            {
                return new AddressRes()
                {
                    UserId = differAddress.UserId,
                    DoorNo = differAddress.DoorNo,
                    PinCode = differAddress.PinCode,
                    AddressId = differAddress.AddressId,
                    City = commonAddress.City,
                    District = commonAddress.District,
                    Nationality = commonAddress.Nationality,
                    Street = differAddress.Street
                };
            }

            throw new Exception("Address not found");
        }

        public AddressRes AddAddress(int userId, AddressReq req)
        {
            throw new System.NotImplementedException();
        }

        public AddressRes UpdateAddress(AddressRes req)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteAddress(int userId, int addressId)
        {
            var address = _dbContext.UserAddress.FirstOrDefault(c=>c.UserId == userId && c.AddressId == addressId);
            if (address!=null)
            {
                _dbContext.UserAddress.Remove(address);

                if (_dbContext.UserAddress.Where(c => c.PinCode == address.PinCode).ToList().Count()<2)
                {
                    var baseAddress=_dbContext.BaseAddress.FirstOrDefault(c => c.PinCode == address.PinCode);
                    if (baseAddress!=null) _dbContext.Remove(baseAddress);
                }

                return true;
            }

            throw new Exception("Contact not found");
        }
    }
}