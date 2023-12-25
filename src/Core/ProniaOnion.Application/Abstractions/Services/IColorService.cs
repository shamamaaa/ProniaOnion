using System;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Colors;

namespace ProniaOnion.Application.Abstractions.Services
{
	public interface IColorService
	{
        Task<ICollection<ItemColorDto>> GetAllAsync(int page, int take);
        Task<GetColorDto> GetByIdAsync(int id);
        Task CreateAsync(CreateColorDto colorDto);
        Task UpdateAsync(int id, UpdateColorDto colorDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseDeleteAsync(int id);

    }
}

