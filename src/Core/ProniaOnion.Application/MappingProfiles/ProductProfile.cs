using System;
using AutoMapper;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Colors;
using ProniaOnion.Application.Dtos.Products;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, ItemProductDto>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            CreateMap<Product, GetProductDto>().ReverseMap();
        }
    }

}

