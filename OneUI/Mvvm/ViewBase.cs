using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace OneUI.Mvvm
{
    public class ViewBase : Page
    {
        /// <summary>
        /// Initializes a new instance of the ViewBase class.
        /// </summary>
        public ViewBase()
        {
            
        }

        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty =
                        DependencyProperty.Register("ViewModel", typeof(ViewModelBase), typeof(ViewBase), new PropertyMetadata(null));

        #endregion

        #region Methods

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if ((ViewModelBase)GetValue(ViewModelProperty) != null)
            {
                ((ViewModelBase)GetValue(ViewModelProperty)).InvokeOnNavigatedFrom(e);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if ((ViewModelBase)GetValue(ViewModelProperty) != null)
            {
                ((ViewModelBase)GetValue(ViewModelProperty)).InvokeOnNavigatedTo(e);
            }
        }

        #endregion
    }
}
