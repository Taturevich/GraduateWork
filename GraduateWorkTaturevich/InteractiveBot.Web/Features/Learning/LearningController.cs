using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLogic.Services;

namespace AimlBotWeb.Features.Learning
{
    public class LearningController : Controller
    {
        private readonly IBotService _botService;
        private readonly IMessageService _messageService;

        public LearningController(
            IBotService botService,
            IMessageService messageService)
        {
            _botService = botService;
            _messageService = messageService;
        }

        // GET: Learning
        [HttpGet]
        public ActionResult Main(LearnMessageModel messageModel)
        {
            return View(messageModel);
        }

        [HttpGet]
        public ActionResult UserDirectory()
        {
            var dictionary = _botService.GetPatternsFromDirectory();
            var mappedDirectory = dictionary.Select(x => new BotPatternsModel
            {
                BotTemplate = x.Key,
                BotPattern = x.Value
            }).ToList();
            return View(mappedDirectory);
        }

        // GET: Learning
        [HttpPost]
        [Authorize]
        [ActionName("Main")]
        public ActionResult FindText(LearnMessageModel messageModel)
        {
            var answer = _botService.GetAnswer(messageModel.UserTypedMessage);
            var newModel = new LearnMessageModel
            {
                BotAnswer = answer.Text,
                UserTypedMessage = messageModel.UserTypedMessage
            };
            return RedirectToAction(nameof(Main), newModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddAnswer(LearnMessageModel messageModel)
        {
            await _botService.CreateAnswer(messageModel.BotAnswer, messageModel.UserTypedMessage);

            return RedirectToAction(nameof(Main), messageModel);
        }
    }
}