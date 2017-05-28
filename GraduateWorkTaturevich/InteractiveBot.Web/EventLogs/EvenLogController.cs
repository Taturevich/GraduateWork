using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> All()
        {
            var eventLogs = await _eventLogService.GetAll();
            var eventLogsModels = eventLogs.Select(Mapper.Map<EventLogModel>);

            return View(eventLogsModels);
        }
    }
}