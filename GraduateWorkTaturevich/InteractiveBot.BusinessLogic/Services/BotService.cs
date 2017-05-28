using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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
        Task<Message> GetAnswer(string input);

        Task CreateAnswer(string template, string pattern);

        Task<Dictionary<string, string>> GetPatternsFromDirectory();
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

        public async Task<Message> GetAnswer(string input)
        {
            var answer = new Message
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Text = _botInit.GetOutput(input),
                SenderType = SenderType.Bot
            };
            await Task.Run(() =>
            {
                _messageService.Add(answer);
            });

            return answer;
        }

        public async Task CreateAnswer(string template, string pattern)
        {
            using (var httpClient = new HttpClient())
            {
                var fileUri = _botInit.GetUserDirectoryRemoteAimlApi;
                var data = await httpClient.GetStringAsync(new Uri(fileUri));
                var doc = XDocument.Parse(data);

                var aimlElement = doc.Element("aiml");
                aimlElement?.Add(new XElement("category",
                    new XElement("template", template),
                    new XElement("pattern", pattern)));
                doc.Save(_botInit.GetUserDirectoryAimlFile);
                var newData = File.ReadAllBytes(_botInit.GetUserDirectoryAimlFile);
                await httpClient.PutAsync(new Uri(_botInit.GetUserDirectoryRemoteAimlApi),
                    new ByteArrayContent(newData));
            }
        }

        public async Task<Dictionary<string, string>> GetPatternsFromDirectory()
        {
            var resultDictionary = new Dictionary<string, string>();

            await Task.Run(() =>
            {
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
            });

            return resultDictionary;
        }
    }
}
