using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities.Domains
{
    [Table("DomainAttributes")]
    public class DomainAttributes
    {
        [Key]
        public int Id { get; set; }

        public virtual Domain Domain { get; set; }

        public int Name { get; set; }
    }
}
