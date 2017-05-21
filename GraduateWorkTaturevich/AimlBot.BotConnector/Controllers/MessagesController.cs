using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Entities.Infrastructure;
using BusinessLogic.Enums;
using BusinessLogic.Infrastructure.Extensions;
using Microsoft.Bot.Connector;
using BusinessLogic.Services;
using Yandex.Speller.Api;
using Yandex.Speller.Api.DataContract;

namespace TestBotConnection.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private readonly IMessageService _messageService;
        private readonly IBotService _botService;

        public MessagesController(
            IMessageService messageService,
            IBotService botService)
        {
            _messageService = messageService;
            _botService = botService;
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                // calculate something for us to return

                var correctMessage = CorrectInputMessage(activity.Text);

                var words = correctMessage.ToWords();

                var parseTest = _messageService.TryParseToDatabaseQuery(words);

                var newMessage = new Message
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Text = correctMessage,
                    SenderType = SenderType.User
                };

                _messageService.Add(newMessage);
                var botAnswer = _botService.GetAnswer(activity.Text);

                // return our reply to the user
                Activity reply = activity.CreateReply($"{botAnswer.Text}");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

        private string CorrectInputMessage(string inputText)
        {
            var correctedmessage = inputText;
            var speller = new YandexSpeller();
            var result = speller.CheckText(correctedmessage, Lang.Ru | Lang.En, Options.Default, TextFormat.Plain);
            int countErrs = result.Errors.Count;
            if (countErrs > 0)
            {
                for (int i = countErrs; i > 0; i--)
                {
                    var err = result.Errors[i - 1];

                    if (err.Steer.Count > 0)
                    {
                        correctedmessage = correctedmessage.Remove(err.Pos, err.Len);
                        correctedmessage = correctedmessage.Insert(err.Pos, err.Steer[0]);
                    }
                };
            }
            return correctedmessage;
        }
    }
}