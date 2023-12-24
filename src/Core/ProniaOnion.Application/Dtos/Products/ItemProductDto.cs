using System;
namespace ProniaOnion.Application.Dtos.Products
{
    public record ItemProductDto(int Id, string Name, decimal Price, string SKU, string? Description);

}

