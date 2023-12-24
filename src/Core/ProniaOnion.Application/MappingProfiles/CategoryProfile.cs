using System;
using AutoMapper;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, ItemCategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<Category, IncludeCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryDto>().ReverseMap();

        }
    }
}

