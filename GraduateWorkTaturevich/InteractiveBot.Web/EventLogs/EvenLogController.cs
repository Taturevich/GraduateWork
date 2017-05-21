using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BusinessLogic.Services;

namespace AimlBotWeb.EventLogs
{
    public class EvenLogController : Controller
    {
        private readonly IEventLogService _eventLogService;

        public EvenLogController(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        // GET: EvenLog
        public ActionResult All()
        {
            var eventLogs = _eventLogService.GetAll();
            var eventLogsModels = eventLogs.Select(Mapper.Map<EventLogModel>);

            return View(eventLogsModels);
        }
    }
}