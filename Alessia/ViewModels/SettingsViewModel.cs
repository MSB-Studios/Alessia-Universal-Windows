using WinUI2 = Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using OneUIX = OneUI.Xaml.Controls;
using Windows.UI.Xaml;
using Alessia.Views;
using OneUI.Mvvm;
using Windows.Storage.Pickers;

namespace Alessia.ViewModels
{
    public sealed class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            filePicker = new FileOpenPicker()
            {
                SuggestedStartLocation = PickerLocationId.MusicLibrary
            };
            filePicker.FileTypeFilter.Add(".mp3");
            filePicker.FileTypeFilter.Add(".wma");
            filePicker.FileTypeFilter.Add(".wav");
            filePicker.FileTypeFilter.Add(".m4a");
        }

        #region Properties

        FileOpenPicker filePicker;

        public FileOpenPicker FilePicker
        {
            get => filePicker;
        }

        #endregion
    }
}
