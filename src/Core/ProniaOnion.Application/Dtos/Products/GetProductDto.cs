﻿using System;
using ProniaOnion.Application.Dtos;

namespace ProniaOnion.Application.Dtos.Products
{
    public record GetProductDto(int Id,string Name, decimal Price, string SKU, string? Description, int CategoryId, IncludeCategoryDto Category);
}

