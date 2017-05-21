using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities.Domains
{
    [Table("Domain")]
    public class Domain
    {
        [Key]
        public int Id { get; set; }

        public virtual DomainArea DomainArea { get; set; }

        public string EntityName { get; set; }
    }
}
