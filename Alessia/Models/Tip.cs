using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace Alessia.Models
{
    [ContentProperty(Name = "Message")]
    public sealed class Tip
    {
        #region Properties

        public string Message { get; set; }

        public ImageSource Image { get; set; }

        #endregion
    }
}
