﻿using System;
namespace ProniaOnion.Domain.Entities
{
	public class ProductTag:BaseEntity
	{
        //Relational properties
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int TagId { get; set; }

        public Tag Tag { get; set; } = null!;
    }
}

