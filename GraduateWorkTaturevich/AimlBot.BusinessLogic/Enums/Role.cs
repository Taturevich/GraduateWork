using System.ComponentModel;

namespace BusinessLogic.Enums
{
    public enum Role
    {
        [Description("Админ")]
        Admin = 0,

        [Description("Гость")]
        Guest = 1,

        [Description("Пользователь")]
        User = 2,

        [Description("Модератор")]
        Moderator = 3,

        [Description("Бот")]
        Bot = 4
    }
}
