using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using BusinessLogic.BotConfiguration;
using BusinessLogic.Entities.Infrastructure;
using BusinessLogic.Enums;
using BusinessLogic.Infrastructure.DAL;

namespace BusinessLogic.Services
{
    public interface IBotService : IEntityServiceBase<Bot>
    {
        Message GetAnswer(string input);

        Task CreateAnswer(string template, string pattern);

        Dictionary<string, string> GetPatternsFromDirectory();
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

        public async Task CreateAnswer(string template, string pattern)
        {
            await Task.Run(() =>
            {
                var fileUri = _botInit.GetUserDirectoryRemoteAimlFile;

                var doc = XDocument.Load(fileUri);
                var aimlElement = doc.Element("aiml");
                aimlElement?.Add(new XElement("category",
                    new XElement("template", template),
                    new XElement("pattern", pattern)));
                doc.Save(_botInit.GetUserDirectoryAimlFile);
                using (var webClient = new WebClient())
                {
                    webClient.UploadFileAsync(
                        new Uri(_botInit.GetUserDirectoryRemoteAimlApi),
                        "PUT",
                        new Uri(_botInit.GetUserDirectoryAimlFile).LocalPath);
                }
            });
        }

        public Dictionary<string, string> GetPatternsFromDirectory()
        {
            var resultDictionary = new Dictionary<string, string>();
            var filePath = _botInit.GetUserDirectoryRemoteAimlFile;
            var doc = XDocument.Load(filePath);
            var aimlElement = doc.Element("aiml");
            var xElements = aimlElement?.Elements("category");
            if (xElements != null)
            {
                foreach (var category in xElements)
                {
                    var xElement = category.Element("template");
                    if (xElement != null)
                    {
                        var element = category.Element("pattern");
                        if (element != null)
                        {
                            resultDictionary.Add(xElement.Value, element.Value);
                        }
                    }
                }
            }

            return resultDictionary;
        }
    }
}
