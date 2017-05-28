using System;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> All()
        {
            var messages = await _messageService.GetAll().ConfigureAwait(false);

            var messageModels = messages.Select(Mapper.Map<MessageModel>);

            return View(messageModels);
        }

        [HttpGet]
        public async Task<ActionResult> Update(string id)
        {
            var message = (await _messageService
                .GetAll().ConfigureAwait(false))
                .FirstOrDefault(x => x.Id == Guid.Parse(id));
            var model = Mapper.Map<MessageModel>(message);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(MessageModel model)
        {
            var message = Mapper.Map<Message>(model);

            await _messageService.Update(message).ConfigureAwait(false);

            return RedirectToAction(nameof(Update));
        }

        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            var message = (await _messageService
                .GetAll().ConfigureAwait(false))
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
        public async Task<ActionResult> Add(MessageModel model)
        {
            var message = Mapper.Map<Message>(model);

            await _messageService.Add(message).ConfigureAwait(false);

            return RedirectToAction(nameof(All));
        }
    }
}