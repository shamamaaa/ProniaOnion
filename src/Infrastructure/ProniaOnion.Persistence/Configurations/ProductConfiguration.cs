﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.SKU).IsRequired().HasMaxLength(10);
        }
    }
}

