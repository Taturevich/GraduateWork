using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities.FactoryDomain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Mark { get; set; }

        public string Sortament { get; set; }

        public string Specification { get; set; }

        public virtual Category Category { get; set; }
    }
}
