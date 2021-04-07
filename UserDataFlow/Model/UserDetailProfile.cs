using System.Collections.Generic;
using AutoMapper;
using UserDataFlow.Model.User;

namespace UserDataFlow.Model
{
    public class UserDetailProfile : Profile
    {
        public UserDetailProfile()
        {
            this.CreateMap<Models.User, UserDetail>()
                .ForMember(o=>o.Contacts,
                    c=>c.MapFrom(v=>v.UserContact))
                .ForMember(o=>o.Addresses,
                    o=>o.MapFrom(v=>v.UserAddress));
        }
    }
}