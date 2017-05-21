using System;
using BusinessLogic.BotConfiguration;
using BusinessLogic.Entities;
using BusinessLogic.Entities.Infrastructure;
using BusinessLogic.Enums;
using BusinessLogic.Infrastructure;

namespace BusinessLogic.Services
{
    public interface IBotService : IEntityServiceBase<Bot>
    {
        Message GetAnswer(string input);
    }

    internal class BotService : EntityServiceBase<Bot>, IBotService
    {
        private readonly BotInitialization _botInit;
        private readonly IRepository<Message> _messageService;

        public BotService(
            IRepository<Bot> repository,
            IRepository<Message> messageService)
            : base(repository)
        {
            _botInit = new BotInitialization();
            _messageService = messageService;
        }

        public Message GetAnswer(string input)
        {
            var answer = new Message
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Text = _botInit.GetOutput(input),
                SenderType = SenderType.Bot
            };
            _messageService.Add(answer);

            return answer;
        }
    }
}
