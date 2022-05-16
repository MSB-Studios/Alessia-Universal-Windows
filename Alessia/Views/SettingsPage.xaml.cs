using Windows.UI.ViewManagement;
using Alessia.ViewModels;
using Windows.UI.Xaml;
using Alessia.Helpers;
using Alessia.Models;
using OneUI.Mvvm;
using System;

namespace Alessia.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : ViewBase
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            SetValue(ViewModelProperty, new SettingsViewModel());
        }

        #region Properties

        public SettingsViewModel ViewModel
        {
            get => (SettingsViewModel)GetValue(ViewModelProperty);
        }

        #endregion

        private async void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            var files = await this.ViewModel.FilePicker.PickMultipleFilesAsync();

            if (files != null)
            {
                foreach (var file in files)
                {
                    LibraryManager.SongList.Add(await Song.CreateFromStorageFile(file));
                }
                await LibraryManager.SaveMusicLibraryAsync();
            }
        }

        private void AddFolderButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
