using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Alessia.Helpers;
using OneUI.Mvvm;
using System;

namespace Alessia.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StarterPage : ViewBase
    {
        public StarterPage()
        {
            this.InitializeComponent();
        }

        #region Methods

        private void IGotItButton_Click(object sender, RoutedEventArgs e)
        {
            ((Storyboard)this.Resources["HideTutorialAnimation"]).Begin();
        }

        private async void CreateLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            ((Storyboard)this.Resources["CreatingLibraryAnimation"]).Begin();
            await LibraryManager.GetSystemMusicLibraryAsync();
            progressTextBlock.Text = "Done.";
            progressBar.Visibility = Visibility.Collapsed;
            progressBar.IsIndeterminate = false;
            await Task.Delay(1500);

            this.NavigateToMainPage();
        }

        private void DoNotCreateLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToMainPage();
        }

        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var flipView = sender as FlipView;
            if (flipView.SelectedIndex == flipView.Items.Count - 1)
            {
                IGotItBtn.Visibility = Visibility.Visible;
            }
            else
            {
                IGotItBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ((Storyboard)this.Resources["HideWelcomeMessageAnimation"]).Begin();
        }

        private void NavigateToMainPage()
        {
            AppDataManager.Current.LocalSettings.Values["IsFirstDeploy"] = false;
            this.Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo()
            {
                Effect = SlideNavigationTransitionEffect.FromBottom
            });
        }

        #endregion
    }
}
