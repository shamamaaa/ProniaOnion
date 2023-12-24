using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Products;
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

        public async Task<ICollection<ItemCategoryDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllWhere(skip: (page - 1) * take, take: take, isTracking: false).ToListAsync();
            var categoryDtos = _mapper.Map<ICollection<ItemCategoryDto>>(categories);
            return categoryDtos;
        }

        public async Task CreateAsync(CreateCategoryDto categoryDto)
        {
            bool result = await _repository.IsExistAsync(c => c.Name == categoryDto.Name);
            if (result) throw new Exception("Already exist");
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

        public async Task SoftDeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");
            _repository.SoftDelete(category);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto categoryDto)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");

            bool result = await _repository.IsExistAsync(c => c.Name == categoryDto.Name);
            if (result) throw new Exception("Already exist");

            _mapper.Map(categoryDto, category);
            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }

        public async Task<GetCategoryDto> GetByIdAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Category item = await _repository.GetByIdAsync(id, includes: nameof(Category.Products));
            if (item == null) throw new Exception("Not Found");

            GetCategoryDto dto = _mapper.Map<GetCategoryDto>(item);

            return dto;
        }
    }
}

