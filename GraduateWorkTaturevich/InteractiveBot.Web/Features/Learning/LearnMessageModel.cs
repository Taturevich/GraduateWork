using System.ComponentModel;

namespace AimlBotWeb.Features.Learning
{
    public class LearnMessageModel
    {
        [DisplayName("Сообщение пользователя")]
        public string UserTypedMessage { get; set; } = string.Empty;

        [DisplayName("Ответ бота")]
        public string BotAnswer { get; set; } = string.Empty;
    }
}