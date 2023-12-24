using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;
        public TagService(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<IemTagDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllWhere(skip: (page - 1) * take, take: take, isTracking: false).ToListAsync();
            var tagDtos = _mapper.Map<ICollection<IemTagDto>>(tags);
            return tagDtos;
        }

        public async Task CreateAsync(CreateTagDto tagDto)
        {
            bool result = await _repository.IsExistAsync(c => c.Name == tagDto.Name);
            if (result) throw new Exception("Already exist");

            await _repository.AddAsync(_mapper.Map<Tag>(tagDto));
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag is null) throw new Exception("Not found");
            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag is null) throw new Exception("Not found");
            _repository.SoftDelete(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateTagDto tagDto)
        {

            Tag tag = await _repository.GetByIdAsync(id);

            bool result = await _repository.IsExistAsync(c => c.Name == tagDto.Name);
            if (result) throw new Exception("Already exist");

            if (tag is null) throw new Exception("Not found");
            _mapper.Map(tagDto, tag);
            _repository.Update(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task<GetTagDto> GetByIdAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Tag item = await _repository.GetByIdAsync(id, includes: $"{nameof(Tag.ProductTags)}.{nameof(ProductTag.Product)}");
            if (item == null) throw new Exception("Not Found");

            GetTagDto dto = _mapper.Map<GetTagDto>(item);

            return dto;
        }
    }
}

