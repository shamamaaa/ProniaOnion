using System;
using ProniaOnion.Application.Dtos;

namespace ProniaOnion.Application.Abstractions.Services
{
	public interface ITagService
	{
        Task<ICollection<IemTagDto>> GetAllAsync(int page, int take);
        Task<GetTagDto> GetByIdAsync(int id);
        Task CreateAsync(CreateTagDto categoryDto);
        Task UpdateAsync(int id, UpdateTagDto categoryDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}

