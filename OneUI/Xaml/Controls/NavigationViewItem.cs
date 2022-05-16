using Windows.UI.Xaml;

namespace OneUI.Xaml.Controls
{
    /// <summary>
    /// Represents an item in a NavigationView control.
    /// </summary>
    public sealed class NavigationViewItem : Primitives.NavigationViewItemBase
    {
        /// <summary>
        /// Initializes a new instance of the NavigationViewItem class.
        /// </summary>
        public NavigationViewItem()
        {
            this.DefaultStyleKey = typeof(NavigationViewItem);
            _ = this.RegisterPropertyChangedCallback(IconProperty, IconChanged_Callback);
            _ = this.RegisterPropertyChangedCallback(IsSelectedProperty, IsSelectedChanged_Callback);
        }

        #region Callbacks

        private static void IconChanged_Callback(DependencyObject d, DependencyProperty e)
        {
            if (d is NavigationViewItem nvItem)
                nvItem.UpdateVisualState();
        }

        private static void IsSelectedChanged_Callback(DependencyObject d, DependencyProperty e)
        {
            if (d is NavigationViewItem nvItem)
                nvItem.UpdateVisualState();
        }

        #endregion

        #region Methods

        protected override void OnApplyTemplate()
        {
            this.UpdateVisualState();
            base.OnApplyTemplate();
        }

        private void UpdateVisualState()
        {
            _ = VisualStateManager.GoToState(this, this.IsSelected ? "NotchVisible" : "NotchCollapsed", true);
            _ = VisualStateManager.GoToState(this, this.Icon != null ? "IconVisible" : "IconCollapsed", false);
        }

        #endregion
    }
}
