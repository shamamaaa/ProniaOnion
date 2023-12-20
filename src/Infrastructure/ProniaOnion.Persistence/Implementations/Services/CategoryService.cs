using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();
            var categoryDtos = _mapper.Map<ICollection<GetCategoryDto>>(categories);
            return categoryDtos;
        }

        public async Task CreateAsync(CreateCategoryDto categoryDto)
        {
            await _repository.AddAsync(_mapper.Map<Category>(categoryDto));
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }


        public async Task UpdateAsync(int id, UpdateCategoryDto categoryDto)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");
            category.Name = categoryDto.Name;
            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }
    }
}

