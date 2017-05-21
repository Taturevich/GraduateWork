using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities.Infrastructure
{
    public class Bot
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}
