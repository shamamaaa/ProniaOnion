using System;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Products;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<ICollection<ItemCategoryDto>> GetAllAsync(int page, int take);
        Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateCategoryDto categoryDto);
        Task UpdateAsync(int id, UpdateCategoryDto categoryDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}

