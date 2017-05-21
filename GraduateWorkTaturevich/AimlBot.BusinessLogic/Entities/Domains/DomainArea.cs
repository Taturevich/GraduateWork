using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities.Domains
{
    [Table("DomainArea")]
    public class DomainArea
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
