using WinUI2 = Windows.UI.Xaml.Controls;
using OneUIX = OneUI.Xaml.Controls;
using OneUI.Mvvm;

namespace Alessia.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            this.Frame = new WinUI2::Frame();
        }

        #region Properties

        public WinUI2::Frame Frame
        {
            get;
        }

        object selectedValue;

        public object SelectedValue
        {
            get => selectedValue;
        }

        object selectedItem;

        public object SelectedItem
        {
            get => selectedItem;
            set
            {
                SetValue(ref selectedItem, value);
                if (value is OneUIX::NavigationViewItem nvItem)
                {
                    SetValue(ref selectedValue, nvItem.Content, "SelectedValue");
                }
            }
        }

        #endregion
    }
}
