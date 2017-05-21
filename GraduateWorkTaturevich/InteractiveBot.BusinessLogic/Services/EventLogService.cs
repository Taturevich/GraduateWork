using BusinessLogic.Entities;
using BusinessLogic.Entities.Infrastructure;
using BusinessLogic.Infrastructure;

namespace BusinessLogic.Services
{
    public interface IEventLogService : IEntityServiceBase<EventLog>
    {
    }
    internal class EventLogService : EntityServiceBase<EventLog>, IEventLogService
    {
        public EventLogService(IRepository<EventLog> repository)
            : base(repository)
        {
        }
    }
}
