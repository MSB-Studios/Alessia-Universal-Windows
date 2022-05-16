using Windows.UI.ViewManagement;
using Alessia.ViewModels;
using Windows.UI.Xaml;
using OneUI.Mvvm;
using OneUIX = OneUI.Xaml.Controls;
using WinUI2 = Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Alessia.Services;
using Windows.Storage.FileProperties;
using System;
using Windows.UI.Popups;

namespace Alessia.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : ViewBase
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            SetValue(ViewModelProperty, new MainViewModel());
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("W: " + e.Size.Width + " | H: " + e.Size.Height);
#endif
        }

        #region Properties

        public MainViewModel ViewModel
        {
            get => (MainViewModel)GetValue(ViewModelProperty);
        }

        #endregion

        #region Methods

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            var navView = sender as OneUIX::NavigationView;

            if (navView.SelectedItem == null)
            {
                foreach (OneUIX::NavigationViewItem item in navView.MenuItems)
                {
                    if ((string)item.Tag == "songs")
                    {
                        this.ViewModel.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void NavigationView_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var item = (OneUIX::NavigationViewItem)this.ViewModel.SelectedItem;
            var transition = new DrillInNavigationTransitionInfo();

            switch (item.Tag as string)
            {
                case "songs":
                    this.ViewModel.Frame.Navigate(typeof(SongsPage), null, transition);
                    break;
                case "settings":
                    this.ViewModel.Frame.Navigate(typeof(SettingsPage), null, transition);
                    break;
            }
        }

        public void MediaPlayerElement_FullScreenRequested(object sender, object args)
        {
            var view = ApplicationView.GetForCurrentView();

            if (view.TryEnterFullScreenMode())
            {
                this.Frame.Navigate(typeof(VisualizerPage));
            }
        }

        private async void MediaPlayerElement_SourceChanged(object sender, object args)
        {
            if (PlaybackService.Current.Source.Thumbnail == null)
            {
                PlaybackService.Current.Source.Thumbnail = await PlaybackService.Current.Source.File.GetThumbnailAsync(ThumbnailMode.MusicView, 350);
            }
            PlaybackService.Current.Thumbnail = PlaybackService.Current.Source.Thumbnail;
        }

        #endregion

        private void MediaPlayerElement_MediaOpened(object sender, object args)
        {

        }

        private void MediaPlayerElement_MediaEnded(object sender, object args)
        {

        }

        private async void MediaPlayerElement_MediaFailed(object sender, object args)
        {
            await new MessageDialog("The selected file could not be found, possibly moved, deleted or renamed, try removing it and adding it back to the library.",
                "Something went wrong!").ShowAsync();
        }
    }
}
