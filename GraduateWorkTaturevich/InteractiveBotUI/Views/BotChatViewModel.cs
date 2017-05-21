using AimlBotUI.Infrastructure;
using AimlBotUI.Shared;
using AimlBotUI.Views.Users;
using BusinessLogic.BotConfiguration.CommandAnalyzer;
using BusinessLogic.Enums;
using BusinessLogic.Infrastructure;
using BusinessLogic.Services;
using Caliburn.Micro;
using System;
using Message = BusinessLogic.Entities.Infrastructure.Message;

namespace AimlBotUI.Views
{
    /// <summary>
    /// Class for main Bot form
    /// </summary>
    [ViewModel]
    public class BotChatViewModel : ScreenBase, IViewModel
    {
        private string _userText;
        private readonly IBotService _botService;
        private readonly IUserService _userService;
        private IUsersViewModelFactory _factory;
        private IWindowManager _windowManager;

        public BotChatViewModel(IBotService botService,
            IUserService userService)
        {
            _botService = botService;
            _userService = userService;
            Items = new BindableCollection<Message>();
            DisplayName = "Bot Application";
        }

        [Inject]
        public void Inject(IUsersViewModelFactory factory, IWindowManager windowManager)
        {
            _factory = factory;
            _windowManager = windowManager;
        }

        /// <summary>
        /// Show Categories form 
        /// </summary>
        public void ShowUsersCommand()
        {
            var usersViewModel = _factory.CreateUsersEditorViewModel();
            _windowManager.ShowWindow(usersViewModel);
        }

        /// <summary>
        /// Message items
        /// </summary>
        public BindableCollection<Message> Items { get; set; }

        public string UserText
        {
            get { return _userText; }

            set
            {
                if (value != _userText)
                {
                    _userText = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        /// <summary>
        /// Save message command
        /// </summary>
        public void OkCommand()
        {
            if (!string.IsNullOrEmpty(UserText) && !string.IsNullOrWhiteSpace(UserText))
            {
                Items.Add(CreateUserMessageModel());
                var answer = _botService.GetAnswer(UserText);
                Items.Add(answer);
                Items.Refresh();
                UserText = string.Empty;
            }
        }

        /// <summary>
        /// Create new message for save
        /// </summary>
        /// <returns>formatted message</returns>
        private Message CreateUserMessageModel()
        {
            var testParser = new CommandParser();
            testParser.Parse();
            var newMessage = new Message
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Text = UserText,
                SenderType = SenderType.User
            };

            return newMessage;
        }
    }
}
