using System;
using ProniaOnion.Application.Dtos;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take);
        //Task<GetCategoryDto> GetAsync(int id);
        Task CreateAsync(CreateCategoryDto categoryDto);
        Task UpdateAsync(int id, UpdateCategoryDto categoryDto);
        Task DeleteAsync(int id);
    }
}

