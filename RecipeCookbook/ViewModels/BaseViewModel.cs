using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace RecipeCookbook.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // holds bool for state
        bool isBusy = false;

        // getter and setter for public access
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        // I need this for Property setting in NewItem viewmodel?
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        // Event watched by refreshviews to refresh their view
        public event PropertyChangedEventHandler PropertyChanged;
        
        // Triggers PropertyChanged event
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
