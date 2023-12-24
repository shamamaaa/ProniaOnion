using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Dtos.Products;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IProductService
	{
        Task<IEnumerable<ItemProductDto>> GetAllPaginated(int page, int take);
        Task<GetProductDto> GetByIdAsync(int id);
        Task CreateAsync(CreateProductDto create);
    }
}

