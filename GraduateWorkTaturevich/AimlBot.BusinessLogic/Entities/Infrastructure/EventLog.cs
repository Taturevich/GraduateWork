using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities.Infrastructure
{
    public class EventLog
    {
        [Key]
        public int Id { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        public string EventCallerName { get; set; }

        public string EventDuration { get; set; }

        public virtual User User { get; set; }
    }
}
