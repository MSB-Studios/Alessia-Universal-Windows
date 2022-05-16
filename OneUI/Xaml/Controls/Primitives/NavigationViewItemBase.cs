using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System;

namespace OneUI.Xaml.Controls.Primitives
{
    public abstract class NavigationViewItemBase : ListBoxItem
    {
        protected NavigationViewItemBase()
        {
            
        }

        #region Properties

        /// <summary>
        /// Gets or sets the icon to show next to the menu item text.
        /// <para>The default is **null**.</para>
        /// </summary>
        public IconElement Icon
        {
            get => (IconElement)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// Gets or sets the icon padding inside the control.
        /// <para>The default is a Thickness with values of 11 on all four sides.</para>
        /// </summary>
        public Thickness IconPadding
        {
            get => (Thickness)GetValue(IconPaddingProperty);
            set => SetValue(IconPaddingProperty, value);
        }

        /// <summary>
        /// Gets or sets the Type to be navigated to when the item is invoked.
        /// <para>The default is **null**.</para>
        /// </summary>
        public Type SourcePageType
        {
            get => (Type)GetValue(SourcePageTypeProperty);
            set => SetValue(SourcePageTypeProperty, value);
        }

        #endregion

        #region Dependency properties

        /// <summary>
        /// Identifies the Icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty =
                    DependencyProperty.Register(nameof(Icon), typeof(IconElement), typeof(NavigationViewItem), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the IconPadding dependency property.
        /// </summary>
        public static readonly DependencyProperty IconPaddingProperty =
                    DependencyProperty.Register(nameof(IconPadding), typeof(Thickness), typeof(NavigationViewItem), new PropertyMetadata(new Thickness(11)));

        /// <summary>
        /// Identifies the SourcePageType dependency property.
        /// </summary>
        public static readonly DependencyProperty SourcePageTypeProperty =
                    DependencyProperty.Register(nameof(SourcePageType), typeof(Type), typeof(NavigationViewItem), new PropertyMetadata(null));

        #endregion
    }
}
