using Windows.Storage;
using OneUI.Mvvm;

namespace Alessia.Helpers
{
    public sealed class AppDataManager : ViewModelBase
    {
        private AppDataManager()
        {

        }

        #region Properties

        bool isTrial;

        /// <summary>
        /// Gets a value that indicates whether the license is a trial license.
        /// </summary>
        public bool IsTrial
        {
            get => isTrial;
            set => SetValue(ref isTrial, value);
        }

        /// <summary>
        /// Gets the application settings container in the local app data store.
        /// </summary>
        public ApplicationDataContainer LocalSettings
        {
            get => ApplicationData.Current.LocalSettings;
        }

        /// <summary>
        /// Gets the root folder in the local app data store. This folder is backed up to the cloud.
        /// </summary>
        public StorageFolder LocalFolder
        {
            get => ApplicationData.Current.LocalFolder;
        }

        /// <summary>
        /// Gets the singleton object class.
        /// </summary>
        public static AppDataManager Current { get; } = new AppDataManager();

        #endregion
    }
}
