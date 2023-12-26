using System;
using AutoMapper;
using ProniaOnion.Application.Dtos.Users;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<RegisterDto, AppUser>().ReverseMap();
        }
    }
}

