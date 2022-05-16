using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Alessia.ViewModels;
using Alessia.Services;
using Windows.UI.Xaml;
using Alessia.Models;
using Windows.System;
using OneUI.Mvvm;
using Alessia.Helpers;
using System;

namespace Alessia.Views
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SongsPage : ViewBase
    {
        public SongsPage()
        {
            this.InitializeComponent();
            SetValue(ViewModelProperty, new SongsViewModel());
        }

        #region Properties

        public SongsViewModel ViewModel
        {
            get => (SongsViewModel)GetValue(ViewModelProperty);
        }

        #endregion

        #region Methods

        private void ListBoxItem_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var song = (sender as ListBox).SelectedItem as Song;

            if (song != null)
            {
                PlaybackService.Current.Source = song;
            }
        }

        private void ListBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                var song = (sender as ListBox).SelectedItem as Song;

                if (song != null)
                {
                    PlaybackService.Current.Source = song;
                }
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var song = (sender as Button).DataContext as Song;

            if (song != null)
            {
                PlaybackService.Current.Source = song;
            }
        }

        private async void DeleteMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var song = (sender as MenuFlyoutItem).DataContext as Song;

            LibraryManager.SongList.Remove(song);
            await LibraryManager.SaveMusicLibraryAsync();
        }

        #endregion
    }
}
