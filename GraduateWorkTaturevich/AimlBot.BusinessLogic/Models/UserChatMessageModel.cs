using BusinessLogic.Enums;
using System;

namespace BusinessLogic.Models
{
    public class UserChatMessageModel
    {
        public Guid Id { get; set; }

        public string NickName { get; set; }

        public string MessageText { get; set; }

        public DateTime MessageDate { get; set; }

        public SenderType SenderType { get; set; }
    }
}
