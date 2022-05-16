using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using OneUI.Mvvm;

namespace Alessia.Views
{
    /// <summary>
    /// A page that displays basic information such as: thumbnail, title and artist of the currently playing song.
    /// </summary>
    public sealed partial class VisualizerPage : ViewBase
    {
        public VisualizerPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        #region Methods

        private void ViewBase_Loading(FrameworkElement sender, object args)
        {
            ((Storyboard)this.Resources["GradientAnimation"]).Begin();
        }

        private void ViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            this.SizeChanged += OnSizeChanged;
        }

        private void ViewBase_Unloaded(object sender, RoutedEventArgs e)
        {
            ((Storyboard)this.Resources["GradientAnimation"]).Stop();
            this.SizeChanged -= OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!ApplicationView.GetForCurrentView().IsFullScreenMode)
            {
                this.Frame.GoBack();
            }
        }

        #endregion
    }
}
