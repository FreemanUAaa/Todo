using AutoMapper;
using System;
using Todo.Users.Core.Interfaces.Mapper;
using Todo.Users.Core.Models;

namespace Todo.Users.Application.Handlers.Users.Queries.GetUserDetails
{
    public class GetUserDetailsVm : IMapWith<User>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public bool IsActivated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, GetUserDetailsVm>()
                .ForMember(x => x.Id,
                    opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Email,
                    opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.UserName,
                    opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.IsActivated,
                    opt => opt.MapFrom(x => x.IsActivated));
        }
    }
}
