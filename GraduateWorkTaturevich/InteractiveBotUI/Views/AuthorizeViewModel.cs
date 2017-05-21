using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AimlBotUI.Infrastructure;
using AimlBotUI.Shared;
using BusinessLogic.Infrastructure;
using BusinessLogic.Services;

namespace AimlBotUI.Views
{
    [ViewModel]
    public class AuthorizeViewModel : ScreenBase
    {
        private IUserService _userService;

        [Inject]
        public void Inject(IUserService userService)
        {
            _userService = userService;
        }
    }
}
