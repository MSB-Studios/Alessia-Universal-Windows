using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Windows.Storage.Search;
using Windows.Foundation;
using Windows.Storage;
using System.Threading.Tasks;
using Alessia.Models;
using System.IO;
using System;

namespace Alessia.Helpers
{
    public static class LibraryManager
    {
        #region Properties

        static ObservableCollection<Song> songList;

        /// <summary>
        /// Gets a songs collection.
        /// </summary>
        public static ObservableCollection<Song> SongList
        {
            get => songList ??= new ObservableCollection<Song>();
        }

        static XmlSerializer serializer;

        private static XmlSerializer Serializer
        {
            get => serializer ??= new XmlSerializer(typeof(ObservableCollection<Song>));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the user's Windows music library and stores it.
        /// </summary>
        /// <returns></returns>
        public static IAsyncAction GetSystemMusicLibraryAsync()
        {
            return Task.Run(async () =>
            {
                var queryOption = new QueryOptions(CommonFileQuery.DefaultQuery, new string[] { ".mp3", ".wma", ".wav", ".m4a" })
                {
                    FolderDepth = FolderDepth.Deep
                };
                var files = await KnownFolders.MusicLibrary.CreateFileQueryWithOptions(queryOption).GetFilesAsync();

                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        SongList.Add(await Song.CreateFromStorageFile(file));
                    }
                    await SaveMusicLibraryAsync();
                }
            }).AsAsyncAction();
        }

        /// <summary>
        /// Loads the stored state of the music library, according to the user's last configuration.
        /// </summary>
        /// <returns>**true** if the load was completed successfully, otherwise it returns **false**.</returns>
        public static IAsyncOperation<bool> LoadMusicLibraryAsync()
        {
            return Task.Run( () =>
            {
                try
                {
                    var fs = new FileStream(ApplicationData.Current.LocalFolder.Path + @"\Library.xml", FileMode.Open);
                    songList = (ObservableCollection<Song>)Serializer.Deserialize(fs);
                    fs.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }).AsAsyncOperation();
        }

        /// <summary>
        /// Saves the current state of the music library.
        /// </summary>
        /// <returns></returns>
        public static IAsyncAction SaveMusicLibraryAsync()
        {
            return Task.Run(() =>
            {
                var writer = new StreamWriter(ApplicationData.Current.LocalFolder.Path + @"\Library.xml");
                Serializer.Serialize(writer, SongList);
                writer.Close();
            }).AsAsyncAction();
        }

        #endregion
    }
}
