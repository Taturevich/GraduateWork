using System;
using BusinessLogic.BotConfiguration.QueryDomainBuilder;
using BusinessLogic.Entities;
using BusinessLogic.Entities.Infrastructure;
using BusinessLogic.Infrastructure;
using BusinessLogic.Infrastructure.DAL;

namespace BusinessLogic.Services
{
    public interface IMessageService : IEntityServiceBase<Message>
    {
        bool TryParseToDatabaseQuery(string[] words);
    }

    internal class MessageService : EntityServiceBase<Message>, IMessageService
    {
        private readonly EntityMatcherProvider _entityMatcherProvider;

        public MessageService(
            IRepository<Message> repository,
            EntityMatcherProvider provider)
            : base(repository)
        {
            _entityMatcherProvider = provider;
        }

        public bool TryParseToDatabaseQuery(string[] words)
        {

            foreach (var word in words)
            {
                var matcher = _entityMatcherProvider.TryGetEntityTypeByTag(word);
                if (matcher != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
