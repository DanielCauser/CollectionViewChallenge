using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CollectionViewChallenge.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private bool _isRefreshing;
        private string _title = string.Empty;

        public virtual bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnError(Exception ex)
        {
            Console.WriteLine(ex);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RunSafe(Action action) => RunSafe(action, OnError);

        protected void RunSafe(Action action, Action<Exception> handleErrorAction)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                handleErrorAction?.Invoke(ex);
            }
        }

        protected Task RunSafeAsync(Func<Task> task) => RunSafeAsync(task, OnError);

        protected async Task RunSafeAsync(Func<Task> task, Action<Exception> handleErrorAction)
        {
            try
            {
                await task().ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                handleErrorAction?.Invoke(ex);
            }
        }

        protected Task<T> RunSafeAsync<T>(Func<Task<T>> task) => RunSafeAsync(task, OnError);

        protected async Task<T> RunSafeAsync<T>(Func<Task<T>> task, Action<Exception> handleErrorAction)
        {
            try
            {
                return await task().ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                handleErrorAction?.Invoke(ex);
            }
            return default(T);
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null, Action onChanged = null, Action<T> onChanging = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            onChanging?.Invoke(value);

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);
            return true;
        }

        public abstract void LoadData();
    }
}
