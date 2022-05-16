using OneUI.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alessia.Helpers
{
    public sealed class BackgroundStatusManager : ViewModelBase
    {
        private BackgroundStatusManager()
        {

        }

        #region Properties

        bool isBussy;

        public bool IsBussy
        {
            get => isBussy;
            set => SetValue(ref isBussy, value);
        }

        /// <summary>
        /// Gets the singleton object class.
        /// </summary>
        public static BackgroundStatusManager Current { get; } = new BackgroundStatusManager();

        #endregion
    }
}
