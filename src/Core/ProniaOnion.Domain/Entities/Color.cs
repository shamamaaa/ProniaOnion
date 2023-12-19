using System;
namespace ProniaOnion.Domain.Entities
{
	public class Color:BaseNameableEntity
	{
        //Relational properties

        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}

