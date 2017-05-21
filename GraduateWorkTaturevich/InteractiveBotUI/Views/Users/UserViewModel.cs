using AimlBotUI.Infrastructure;
using BusinessLogic.Entities;
using BusinessLogic.Entities.Infrastructure;
using BusinessLogic.Enums;
using BusinessLogic.Infrastructure;

namespace AimlBotUI.Views.Users
{
    public class UserViewModel : ChildViewEntityBase<User>
    {
        private IUsersViewModelFactory _factory;

        public UserViewModel()
        {
        }

        public UserViewModel(User user)
            : base(user)
        {
        }

        [Inject]
        public void Inject(IUsersViewModelFactory factory)
        {
            _factory = factory;
        }

        public string Name
        {
            get { return Model.Name; }
            set
            {
                SetModelProperty(value);
                NotifyOfPropertyChange();
            }
        }

        public Role Role
        {
            get { return Model.Role; }
            set
            {
                SetModelProperty(value);
                NotifyOfPropertyChange();
            }
        }

        protected override object CloneInternal()
        {
            var user = new User();
            CopyProperties(Model, user);
            return _factory.CreateUserViewModel(user);
        }
    }
}
