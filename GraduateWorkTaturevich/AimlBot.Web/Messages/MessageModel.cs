using System;
using System.ComponentModel;

namespace AimlBotWeb.Messages
{
    public class MessageModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [DisplayName("Дата")]
        public DateTime Date { get; set; } = DateTime.Now;

        [DisplayName("Текст")]
        public string Text { get; set; }

        [DisplayName("Тип отправителя")]
        public string SenderType { get; set; }

        [DisplayName("Имя отправителя")]
        public string UserName { get; set; }
    }
}