using System;
using System.Diagnostics;
using BusinessLogic.Services;
using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception.Request;
using EventLog = BusinessLogic.Entities.Infrastructure.EventLog;

namespace BusinessLogic.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class BotEventLogAttribute : InterceptAttribute
    {
        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            return request.Kernel.Get<EventLogInterceptor>();
        }
    }
    internal class EventLogInterceptor : IInterceptor
    {
        private readonly IEventLogService _eventLogService;
        public EventLogInterceptor(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }
        public void Intercept(IInvocation invocation)
        {
            var watch = new Stopwatch();
            watch.Start();
            invocation.Proceed();
            watch.Stop();
            var newEventLog = new EventLog
            {
                EventCallerName = invocation.Request.Method.Name,
                EventDate = DateTime.Now,
                EventDuration = watch.ElapsedMilliseconds.ToString(),
                Description = invocation.Request.Method.ToString()
            };
            _eventLogService.Add(newEventLog);
        }
    }
}
