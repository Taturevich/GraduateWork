using System.ComponentModel;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace AimlBotUI.Infrastructure
{
    public abstract class MasterDetailBase<TChild> : MasterDetailBase<TChild, TChild>
        where TChild : class, INotifyDataErrorInfo, INotifyPropertyChangedEx, IChild, IHasChanges, IMutateMode
    {
        public virtual TChild OperativeItem
        {
            get
            {
                return DetailItem;
            }

            set
            {
                DetailItem = value;
            }
        }

        protected abstract TChild NewChildInstance();

        protected sealed override void OnDetailItemChanged(TChild previous)
        {
            base.OnDetailItemChanged(previous);

            NotifyOfPropertyChange();
            OnOperativeItemChanged(previous);
        }

        protected virtual void OnOperativeItemChanged(TChild previous)
        {
        }

        protected sealed override void OnDetailItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnDetailItemPropertyChanged(sender, e);

            OnOperativeItemPropertyChanged(sender, e);
        }

        protected virtual void OnOperativeItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
        }

        protected sealed override void OnDetailItemErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            base.OnDetailItemErrorsChanged(sender, e);

            OnOperativeItemErrorsChanged(sender, e);
        }

        protected virtual void OnOperativeItemErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
        }

        protected sealed override Task<TChild> CreateDetailItem(TChild masterItem)
        {
            return Task.FromResult(CloneItem(masterItem));
        }

        protected TChild CloneItem(TChild item = default(TChild))
        {
            var result = item != null ? (TChild)item.Clone() : NewChildInstance();
            result.ErrorsChanged += OnDetailItemErrorsChanged;
            result.PropertyChanged += OnDetailItemPropertyChanged;
            return result;
        }
    }
    public abstract class MasterDetailBase<TMasterViewModel, TDetailViewModel> : ScreenBase
         where TDetailViewModel : class, INotifyPropertyChangedEx, IHasChanges, INotifyDataErrorInfo
    {
        private BindableCollection<TMasterViewModel> _items;
        private TDetailViewModel _detailItem;
        private TMasterViewModel _selectedItem;

        public virtual BindableCollection<TMasterViewModel> Items
        {
            get { return _items; }

            set
            {
                if (SetProperty(ref _items, value))
                {
                    OnItemsChanged();
                }
            }
        }

        public TMasterViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                SetProperty(ref _selectedItem, value);
                OnSelectedItemChanged();
            }
        }

        public TDetailViewModel DetailItem
        {
            get
            {
                return _detailItem;
            }

            set
            {
                var previous = _detailItem;
                if (SetProperty(ref _detailItem, value))
                {
                    OnDetailItemChanged(previous);
                }
            }
        }

        protected abstract Task<TDetailViewModel> CreateDetailItem(TMasterViewModel masterItem);

        protected virtual async void OnSelectedItemChanged()
        {
            DetailItem = await CreateDetailItem(SelectedItem);
        }

        protected virtual void OnDetailItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "HasChanges" && !string.IsNullOrEmpty(e.PropertyName))
            {
                DetailItem.HasChanges = true;
            }
        }

        protected virtual void OnItemsChanged()
        {
        }

        protected virtual void OnDetailItemChanged(TDetailViewModel previous)
        {
            if (previous != null)
            {
                previous.ErrorsChanged -= OnDetailItemErrorsChanged;
                previous.PropertyChanged -= OnDetailItemPropertyChanged;
            }
        }

        protected virtual void OnDetailItemErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
        }
    }
}
