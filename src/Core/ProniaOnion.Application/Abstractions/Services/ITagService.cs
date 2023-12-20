using System;
using ProniaOnion.Application.Dtos;

namespace ProniaOnion.Application.Abstractions.Services
{
	public interface ITagService
	{
        Task<ICollection<GetTagDto>> GetAllAsync(int page, int take);
        //Task<GetCategoryDto> GetAsync(int id);
        Task CreateAsync(CreateTagDto categoryDto);
        Task UpdateAsync(int id, UpdateTagDto categoryDto);
        Task DeleteAsync(int id);
    }
}

