using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Configurations
{
	public class CategoryConfiguration:IEntityTypeConfiguration<Category>
	{
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}

