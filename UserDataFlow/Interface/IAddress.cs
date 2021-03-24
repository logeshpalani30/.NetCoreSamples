using System.Collections.Generic;
using UserDataFlow.Model.Address;

namespace UserDataFlow.Interface
{
    public interface IAddress
    {
        List<AddressRes> GetAddress(int userId);
        AddressRes GetAddress(int userId, int addressId);

        AddressRes AddAddress(int userId, AddressReq req);
        AddressRes UpdateAddress(AddressRes req);
        bool DeleteAddress(int userId, int addressId);
    }
}