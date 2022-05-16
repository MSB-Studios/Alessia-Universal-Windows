using System.Collections.ObjectModel;
using Alessia.Helpers;
using Alessia.Models;
using OneUI.Mvvm;

namespace Alessia.ViewModels
{
    public sealed class SongsViewModel : ViewModelBase
    {
        public SongsViewModel()
        {

        }

        #region Properties

        public ObservableCollection<Song> SongList
        {
            get => LibraryManager.SongList;
        }

        #endregion
    }
}
