using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities.Infrastructure
{
    public class Password
    {
        [Key]
        public int Id { get; set; }

        public string Hash { get; set; }

        public virtual User User { get; set; }
    }
}
