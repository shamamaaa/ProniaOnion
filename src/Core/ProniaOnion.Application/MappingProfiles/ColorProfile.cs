using System;
using AutoMapper;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Colors;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    public class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<Color, ItemColorDto>().ReverseMap();
            CreateMap<CreateColorDto, Color>();
            CreateMap<UpdateColorDto, Color>().ReverseMap();
            CreateMap<Color, GetColorDto>().ReverseMap();
        }
    }
}

