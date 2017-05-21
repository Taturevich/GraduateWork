using System;
using System.ComponentModel.DataAnnotations;
using BusinessLogic.Enums;

namespace BusinessLogic.Entities.Infrastructure
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        public SenderType SenderType { get; set; }

        public virtual User User { get; set; }
    }
}
