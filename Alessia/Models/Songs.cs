using Windows.Storage.FileProperties;
using System.Xml.Serialization;
using System.Threading.Tasks;
using OneUI.Media.Playback;
using Windows.Foundation;
using Windows.Media.Core;
using Windows.Storage;
using System;

namespace Alessia.Models
{
    public sealed class Song : IMusicPlayback
    {
        #region Properties

        [XmlIgnore]
        public int Index { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public string Genre { get; set; }

        public string Year { get; set; }

        public TimeSpan Duration { get; set; }

        public string Path { get; set; }

        [XmlIgnore]
        public StorageFile File { get; set; }

        [XmlIgnore]
        public MediaSource MediaSource { get; set; }

        [XmlIgnore]
        public StorageItemThumbnail Thumbnail { get; set; }

        #endregion

        #region Methods

        public static IAsyncOperation<Song> CreateFromStorageFile(StorageFile storageFile)
        {
            return Task.Run(async () =>
            {
                MusicProperties properties = await storageFile.Properties.GetMusicPropertiesAsync();
                return new Song()
                {
                    Title = string.IsNullOrEmpty(properties.Title) ? storageFile.DisplayName : properties.Title,
                    Artist = string.IsNullOrEmpty(properties.AlbumArtist) ? "Unknown artist" : properties.AlbumArtist,
                    Album = string.IsNullOrEmpty(properties.Album) ? "Unknown album" : properties.Album,
                    Genre = properties.Genre.Count > 0 ? properties.Genre[0] : "",
                    Year = properties.Year == 0 ? "" : properties.Year.ToString(),
                    Duration = properties.Duration,
                    Path = storageFile.Path,
                    File = storageFile
                };
            }).AsAsyncOperation();
        }

        #endregion
    }
}
