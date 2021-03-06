﻿using BusinessLogic.Entities.Infrastructure;
using BusinessLogic.Enums;
using BusinessLogic.Infrastructure.Extensions;
using BusinessLogic.Models;
using BusinessLogic.Services;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
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
                var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                // calculate something for us to return

                var correctMessage = CorrectInputMessage(activity.Text);

                var words = correctMessage.ToWords();

                var container = await _messageService.TryParseToDatabaseQuery(words);

                var newMessage = new Message
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Text = correctMessage,
                    SenderType = SenderType.User
                };
                await _messageService.Add(newMessage);


                if (container.IsContainsDomainData)
                {
                    var taskList = container
                        .DomainDataList
                        .Select(x => ReplyToUser(x, activity, connector))
                        .ToList();
                    await Task.WhenAll(taskList).ConfigureAwait(false);
                }
                else
                {
                    var botAnswer = await _botService.GetAnswer(activity.Text);
                    var answerText = botAnswer.Text ?? string.Empty;
                    var reply = activity.CreateReply($"{answerText}");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }

                // return our reply to the user
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async Task ReplyToUser(
            DisplayedObject displayedObject,
            Activity activity,
            ConnectorClient connector)
        {
            var reply = activity.CreateReply(displayedObject.DisplayInformation);
            reply.Attachments = new List<Attachment>
            {
                new Attachment
                {
                    ContentUrl = Url.Content($"~/content/{displayedObject.ImageName}"),
                    ContentType = "image/png",
                    Name = $"{displayedObject.ImageName}"
                }
            };
            await connector.Conversations.ReplyToActivityAsync(reply);
        }

        private void HandleSystemMessage(Activity message)
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
                }
            }
            return correctedmessage;
        }
    }
}