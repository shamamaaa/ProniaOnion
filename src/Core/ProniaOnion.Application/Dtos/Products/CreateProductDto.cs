using System;
namespace ProniaOnion.Application.Dtos.Products
{
    public record CreateProductDto(string Name, decimal Price, string SKU, string? Description, int CategoryId, ICollection<int> ColorIds);
}

