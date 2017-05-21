using BusinessLogic.Entities;
using BusinessLogic.Entities.Infrastructure;

namespace AimlBotUI.Views.Users
{
    public interface IUsersViewModelFactory
    {
        UsersEditorViewModel CreateUsersEditorViewModel();

        UserViewModel CreateUserViewModel();

        UserViewModel CreateUserViewModel(User user);
    }
}
