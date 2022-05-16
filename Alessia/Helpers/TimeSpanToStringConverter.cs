using Windows.UI.Xaml.Data;
using System;

namespace Alessia.Helpers
{
    public sealed class TimeSpanToStringConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is not null && value is TimeSpan ts)
            {
                return ts.ToString(@"mm\:ss");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
