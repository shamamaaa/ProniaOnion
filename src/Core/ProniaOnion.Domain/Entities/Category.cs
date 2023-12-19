using System;
namespace ProniaOnion.Domain.Entities
{
	public class Category:BaseNameableEntity
	{
        //Relational properties

        public ICollection<Product>? Products { get; set; }

	}
}

