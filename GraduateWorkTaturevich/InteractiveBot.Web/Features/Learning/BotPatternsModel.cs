using System.ComponentModel;

namespace AimlBotWeb.Features.Learning
{
    public class BotPatternsModel
    {
        [DisplayName("Паттерн")]
        public string BotPattern { get; set; }

        [DisplayName("Шаблон ответа")]
        public string BotTemplate { get; set; }
    }
}