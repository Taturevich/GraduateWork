﻿using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using BusinessLogic.Entities.Infrastructure;
using BusinessLogic.Services;

namespace AimlBotWeb.Messages
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET: Messages
        [OutputCache(Duration = 10, Location = OutputCacheLocation.Client)]
        public ActionResult All()
        {
            var messages = _messageService.GetAll();

            var messageModels = messages.Select(Mapper.Map<MessageModel>);

            return View(messageModels);
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            var message = _messageService
                .GetAll()
                .FirstOrDefault(x => x.Id == Guid.Parse(id));
            var model = Mapper.Map<MessageModel>(message);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(MessageModel model)
        {
            var message = Mapper.Map<Message>(model);

            _messageService.Update(message);

            return RedirectToAction(nameof(Update));
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var message = _messageService
                .GetAll()
                .FirstOrDefault(x => x.Id == Guid.Parse(id));
            var model = Mapper.Map<MessageModel>(message);
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new MessageModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MessageModel model)
        {
            var message = Mapper.Map<Message>(model);

            _messageService.Add(message);

            return RedirectToAction(nameof(All));
        }
    }
}