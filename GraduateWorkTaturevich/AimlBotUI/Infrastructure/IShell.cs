using System;
using AimlBotUI.Shared;

namespace AimlBotUI.Infrastructure
{
    public interface IShell : IViewModel
    {
        Action LoginSuccessful { get; set; }
        Action Logout { get; set; }
    }
}
