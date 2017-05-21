using Caliburn.Micro;
using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BusinessLogic.Entities;
using BusinessLogic.Entities.Infrastructure;
using Action = System.Action;

namespace AimlBotUI.Infrastructure
{
    public abstract class ScreenBase : Screen, INotifyDataErrorInfo, IShell
    {
        private bool _isBusy;

        private bool _isConfirm;

        public ScreenBase()
        {
            LoginSuccessful = delegate { };
            Logout = delegate { };
        }

        /// <summary>
        /// Get or set status of busy indicator for background tasks 
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Callback state from child forms
        /// </summary>
        public bool IsConfirm
        {
            get { return _isConfirm; }
            set
            {
                if (_isConfirm == value) return;
                _isConfirm = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Get or set current application user
        /// </summary>
        public static User CurrentUser { get; set; }

        protected bool SetProperty<TP>(ref TP storage, TP value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            NotifyOfPropertyChange(propertyName);
            return true;
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        public bool HasErrors { get; }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public Action LoginSuccessful { get; set; }
        public Action Logout { get; set; }
    }
}
