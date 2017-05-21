using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BusinessLogic.Entities.Infrastructure;

namespace AimlBotWeb.EventLogs
{
    public class EventLogModel
    {
        public int Id { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        public string EventCallerName { get; set; }

        public string EventDuration { get; set; }
    }
}