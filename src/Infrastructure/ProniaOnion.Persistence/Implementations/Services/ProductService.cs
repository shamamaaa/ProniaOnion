using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Products;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
	public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemProductDto>> GetAllPaginated(int page, int take)
        {
            IEnumerable<ItemProductDto> dtos = _mapper.Map<IEnumerable<ItemProductDto>>(await _repository.GetAllWhere(skip: (page - 1) * take, take: take, isTracking: false).ToArrayAsync());
            return dtos;
        }

        public async Task<GetProductDto> GetByIdAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Product item = await _repository.GetByIdAsync(id, includes: nameof(Product.Category));
            if (item == null) throw new Exception("Not Found");

            GetProductDto dto = _mapper.Map<GetProductDto>(item);

            return dto;
        }

        public async Task CreateAsync(CreateProductDto productDto)
        {
            bool result = await _repository.IsExistAsync(c => c.Name == productDto.Name);
            if (result) throw new Exception("Already Exist");

            await _repository.AddAsync(_mapper.Map<Product>(productDto));
            await _repository.SaveChangesAsync();
        }

    }
}


