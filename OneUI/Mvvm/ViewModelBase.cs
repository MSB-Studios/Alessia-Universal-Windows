using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;

namespace OneUI.Mvvm
{
    public class ViewModelBase : INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelBase class.
        /// </summary>
        public ViewModelBase()
        {
            
        }

        #region Methods

        /// <summary>
        /// Checks if a property already matches the desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="field">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="name">Name of the property used to notify listeners.</param>
        /// <returns>**true** if the value was changed, **false** if the existing value matched the
        /// desired value.</returns>
        protected bool SetValue<T>(ref T field, T value, [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
                field = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Property " + name + " has been updated.");
#endif
                return true;
            }
            return false;
        }

        internal void InvokeOnNavigatedFrom(NavigationEventArgs e) => this.OnNavigatedFrom(e);

        /// <summary>
        /// Invoked immediately after the Page is unloaded and is no longer the current source
        /// of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative
        /// of the navigation that has unloaded the current Page.</param>
        /// <returns></returns>
        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
        {

        }

        internal void InvokeOnNavigatedTo(NavigationEventArgs e) => this.OnNavigatedTo(e);

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative
        /// of the pending navigation that will load the current Page. Usually the most relevant
        /// property to examine is Parameter.</param>
        /// <returns></returns>
        protected virtual void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        #endregion

        #region Events

        /// <inheritdoc />
        public event PropertyChangingEventHandler PropertyChanging;

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
