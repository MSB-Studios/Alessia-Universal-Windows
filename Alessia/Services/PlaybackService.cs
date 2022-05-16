using Windows.Storage.FileProperties;
using System.Collections.ObjectModel;
using Alessia.Models;
using OneUI.Mvvm;

namespace Alessia.Services
{
    public sealed class PlaybackService : ViewModelBase
    {
        private PlaybackService()
        {
            this.PreviousList = new ObservableCollection<Song>();
            this.NextUpList = new ObservableCollection<Song>();
        }

        #region Properties

        Song source;

        public Song Source
        {
            get => source;
            set => SetValue(ref source, value);
        }

        StorageItemThumbnail thumbnail;

        public StorageItemThumbnail Thumbnail
        {
            get => thumbnail;
            set => SetValue(ref thumbnail, value);
        }

        ObservableCollection<Song> collectionSource;

        public ObservableCollection<Song> CollectionSource
        {
            get => collectionSource;
            set => SetValue(ref collectionSource, value);
        }

        public ObservableCollection<Song> PreviousList
        {
            get;
        }

        public ObservableCollection<Song> NextUpList
        {
            get;
        }

        /// <summary>
        /// Gets the singleton object class.
        /// </summary>
        public static PlaybackService Current { get; } = new PlaybackService();

        #endregion
    }
}
