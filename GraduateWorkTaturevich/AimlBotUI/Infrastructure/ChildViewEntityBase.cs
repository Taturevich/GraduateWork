using System;
using System.Runtime.CompilerServices;

namespace AimlBotUI.Infrastructure
{
    public abstract class ChildViewEntityBase<T> : ChildViewModelBase, IHasChanges
        where T : new()
    {
        private T _model;

        public T Model => _model;

        protected ChildViewEntityBase()
        {
            _model = new T();
        }

        protected ChildViewEntityBase(T model)
        {
            _model = model;
        }

        protected void SetModel(T model)
        {
            _model = model;
            Refresh();
        }

        protected void SetModelProperty(object value, [CallerMemberName]string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var modelPropertyInfo = Model.GetType().GetProperty(propertyName);
            var oldValue = modelPropertyInfo.GetValue(Model);
            if (!Equals(oldValue, value))
            {
                modelPropertyInfo.SetValue(Model, value);
                NotifyOfPropertyChange(propertyName);
            }
        }

        public bool HasChanges { get; set; }
    }
}
