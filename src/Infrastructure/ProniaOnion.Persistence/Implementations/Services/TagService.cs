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

        public async Task<ICollection<GetTagDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();
            var tagDtos = _mapper.Map<ICollection<GetTagDto>>(tags);
            return tagDtos;
        }

        public async Task CreateAsync(CreateTagDto tagDto)
        {
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

        public async Task UpdateAsync(int id, UpdateTagDto tagDto)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag is null) throw new Exception("Not found");
            tag.Name = tagDto.Name;
            _repository.Update(tag);
            await _repository.SaveChangesAsync();
        }
    }
}

