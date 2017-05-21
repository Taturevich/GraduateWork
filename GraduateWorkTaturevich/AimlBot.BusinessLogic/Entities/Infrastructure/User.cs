using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessLogic.Enums;

namespace BusinessLogic.Entities.Infrastructure
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public Role Role { get; set; }

        public string Name { get; set; }
    }
}
