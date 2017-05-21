using System;
using System.Collections.Generic;
using System.Linq;
using AimlBotUI.Infrastructure;
using AimlBotUI.Shared;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Infrastructure;
using BusinessLogic.Services;
using Caliburn.Micro;

namespace AimlBotUI.Views.Users
{
    [ViewModel]
    public class UsersEditorViewModel : MasterDetailBase<UserViewModel>, IOperations
    {
        private IUsersViewModelFactory _factory;
        private IUserService _userService;
        private UserViewModel _currentOperativeUser;

        private Role _selectedUserRole;

        protected override void OnViewLoaded(object view)
        {
            RefreshItemList();
            if (!Items.Any())
            {
                CurrentOperativeUser = NewChildInstance();
            }
            DisplayName = "Пользователи";
        }

        [Inject]
        public void Inject(IUsersViewModelFactory factory, IUserService userService)
        {
            _factory = factory;
            _userService = userService;
            Roles = Enum.GetValues(typeof(Role)).Cast<Role>();
        }

        protected override void OnOperativeItemChanged(UserViewModel previous)
        {
            base.OnOperativeItemChanged(previous);
            CurrentOperativeUser = OperativeItem;
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            SelectedUserRole = OperativeItem.Role;
        }

        public IEnumerable<Role> Roles { get; set; }

        public Role SelectedUserRole
        {
            get { return _selectedUserRole; }
            set
            {
                SetProperty(ref _selectedUserRole, value);
                CurrentOperativeUser.Model.Role = SelectedUserRole;
            }
        }

        public UserViewModel CurrentOperativeUser
        {
            get { return _currentOperativeUser; }
            set
            {
                _currentOperativeUser = value;
                NotifyOfPropertyChange();
            }
        }

        protected override UserViewModel NewChildInstance()
        {
            return _factory.CreateUserViewModel();
        }

        public void SaveCommand()
        {
            _userService.Add(CurrentOperativeUser.Model);
            RefreshItemList();

        }

        public void DeleteCommand()
        {
            _userService.Delete(CurrentOperativeUser.Model);
            RefreshItemList();
        }

        public void UpdateCommand()
        {
            var clone = CurrentOperativeUser.Clone() as UserViewModel;
            if (clone != null)
            {
                _userService.Update(clone.Model);
                RefreshItemList();
            }
        }

        public void RefreshItemList()
        {
            Items = new BindableCollection<UserViewModel>(_userService.GetAll().Select(x => _factory.CreateUserViewModel(x)));
        }
    }
}
