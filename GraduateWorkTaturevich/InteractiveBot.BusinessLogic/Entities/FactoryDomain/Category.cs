using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities.FactoryDomain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public override string ToString() => $"Категория *{Name}*\n\n";
    }
}
