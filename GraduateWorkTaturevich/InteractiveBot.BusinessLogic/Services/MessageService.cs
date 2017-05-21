using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.BotConfiguration.QueryDomainBuilder;
using BusinessLogic.Entities.FactoryDomain;
using BusinessLogic.Entities.Infrastructure;
using BusinessLogic.Infrastructure.DAL;
using BusinessLogic.Infrastructure.Extensions;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface IMessageService : IEntityServiceBase<Message>
    {
        ReplyContainer TryParseToDatabaseQuery(string[] words);
    }

    internal class MessageService : EntityServiceBase<Message>, IMessageService
    {
        private readonly IDatabaseCommandContext _commandContext;
        private readonly EntityMatcherProvider _entityMatcherProvider;

        public MessageService(
            IRepository<Message> repository,
            EntityMatcherProvider provider,
            IDatabaseCommandContext commandContext)
            : base(repository)
        {
            _commandContext = commandContext;
            _entityMatcherProvider = provider;
        }

        public ReplyContainer TryParseToDatabaseQuery(string[] words)
        {
            var container = new ReplyContainer();
            foreach (var word in words)
            {
                var matcher = _entityMatcherProvider.TryGetEntityTypeByTag(word);
                if (matcher != null)
                {
                    if (matcher.GetEntityType == typeof(Product))
                    {
                        var commandresult = GetListOfProducts(matcher);
                        container
                            .DomainDataList
                            .AddRange(
                                commandresult
                                .Select(x => new DisplayedObject
                                {
                                    DisplayInformation = x.ToString(),
                                    ImageName = x.ImageName
                                }));
                    }

                    if (matcher.GetEntityType == typeof(Category))
                    {
                        var commandresult = GetListOfCategories(matcher);
                        container
                            .DomainDataList
                            .AddRange(
                                commandresult
                                .Select(x => new DisplayedObject
                                {
                                    DisplayInformation = x.ToString(),
                                    ImageName = x.ImageName
                                }));
                    }
                }
            }

            return container;
        }

        private List<Product> GetListOfProducts(EntityMatcher matcher)
        {
            var command = $"SELECT * FROM {matcher.TableName}";
            var result = _commandContext
                .ExecuteTypeSqlCommand<Product>(command)
                .ToList();

            return result;
        }

        private List<Category> GetListOfCategories(EntityMatcher matcher)
        {
            var command = $"SELECT * FROM {matcher.TableName}";
            var result = _commandContext
                .ExecuteTypeSqlCommand<Category>(command)
                .ToList();

            return result;
        }
    }
}
