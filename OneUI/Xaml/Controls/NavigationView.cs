using System.Collections.ObjectModel;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.Foundation;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Markup;

namespace OneUI.Xaml.Controls
{
    /// <summary>
    /// Represents a container that enables navigation of app content. It has a header,
    /// a view for the main content, and a menu pane for navigation commands.
    /// </summary>
    [ContentProperty(Name = "Content")]
    public sealed class NavigationView : Control
    {
        /// <summary>
        /// Initializes a new instance of the NavigationView class.
        /// </summary>
        public NavigationView()
        {
            this.DefaultStyleKey = typeof(NavigationView);
        }

        #region Properties

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        /// <summary>
        /// Gets the collection of menu items displayed in the NavigationMenu.
        /// <para>The default is an empty collection.</para>
        /// </summary>
        public IList<object> MenuItems
        {
            get => (ObservableCollection<object>)GetValue(MenuItemsProperty);
        }

        /// <summary>
        /// .
        /// <para>The default is an empty collection.</para>
        /// </summary>
        public IList<object> FooterItems
        {
            get => (ObservableCollection<object>)GetValue(FooterItemsProperty);
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// <para>The default is **null**.</para>
        /// </summary>
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        /// Gets or sets the header content.
        /// <para>The default is **null**.</para>
        /// </summary>
        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        /// <summary>
        /// Gets or sets the content for the pane header.
        /// <para>The default is **null**.</para>
        /// </summary>
        public object PaneHeader
        {
            get => GetValue(PaneHeaderProperty);
            set => SetValue(PaneHeaderProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of the NavigationView pane in its compact display mode.
        /// <para>The default is 48 device-independent pixel (DIP).</para>
        /// </summary>
        public double CompactPaneLength
        {
            get => (double)GetValue(CompactPaneLengthProperty);
            set => SetValue(CompactPaneLengthProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of the NavigationView pane when it's fully expanded.
        /// <para>The default is 220 device-independent pixel (DIP).</para>
        public double OpenPaneLength
        {
            get => (double)GetValue(OpenPaneLengthProperty);
            set => SetValue(OpenPaneLengthProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that specifies whether the NavigationView pane is expanded to its full width.
        /// <para>The default is **false**.</para>
        /// </summary>
        public bool IsPaneOpen
        {
            get => (bool)GetValue(IsPaneOpenProperty);
            set => SetValue(IsPaneOpenProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the back button is enabled or disabled.
        /// <para>The default is **false**.</para>
        /// </summary>
        public bool IsBackButtonEnabled
        {
            get => (bool)GetValue(IsBackButtonEnabledProperty);
            set => SetValue(IsBackButtonEnabledProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the back button is shown.
        /// <para>The default is **Visible**.</para>
        /// </summary>
        public Visibility BackButtonVisibility
        {
            get => (Visibility)GetValue(BackButtonVisibilityProperty);
            set => SetValue(BackButtonVisibilityProperty, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the menu toggle button is shown.
        /// <para>The default is **Visible**.</para>
        /// </summary>
        public Visibility PaneToggleButtonVisibility
        {
            get => (Visibility)GetValue(PaneToggleButtonVisibilityProperty);
            set => SetValue(PaneToggleButtonVisibilityProperty, value);
        }

        #endregion

        #region Dependency properties

        /// <summary>
        /// Identifies the Content dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
                    DependencyProperty.Register(nameof(Content), typeof(object), typeof(NavigationView), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the MenuItems dependency property.
        /// </summary>
        public static readonly DependencyProperty MenuItemsProperty =
                    DependencyProperty.Register(nameof(MenuItems), typeof(IList<object>), typeof(NavigationView), new PropertyMetadata(new ObservableCollection<object>()));

        /// <summary>
        /// Identifies the FooterItems dependency property.
        /// </summary>
        public static readonly DependencyProperty FooterItemsProperty =
                    DependencyProperty.Register(nameof(FooterItems), typeof(IList<object>), typeof(NavigationView), new PropertyMetadata(new ObservableCollection<object>()));

        /// <summary>
        /// Identifies the SelectedItem dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
                    DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(NavigationView), new PropertyMetadata(null, SelectedItemChanged_Callback));

        /// <summary>
        /// Identifies the Header dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
                    DependencyProperty.Register(nameof(Header), typeof(object), typeof(NavigationView), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the PaneHeader dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneHeaderProperty =
                    DependencyProperty.Register(nameof(PaneHeader), typeof(object), typeof(NavigationView), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the CompactPaneLength dependency property.
        /// </summary>
        public static readonly DependencyProperty CompactPaneLengthProperty =
                    DependencyProperty.Register(nameof(CompactPaneLength), typeof(double), typeof(NavigationView), new PropertyMetadata(50d));

        /// <summary>
        /// Identifies the OpenPaneLength dependency property.
        /// </summary>
        public static readonly DependencyProperty OpenPaneLengthProperty =
                    DependencyProperty.Register(nameof(OpenPaneLength), typeof(double), typeof(NavigationView), new PropertyMetadata(220d));

        /// <summary>
        /// Identifies the IsPaneOpen dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPaneOpenProperty =
                    DependencyProperty.Register(nameof(IsPaneOpen), typeof(bool), typeof(NavigationView), new PropertyMetadata(false, IsPaneOpenChanged_Callback));

        /// <summary>
        /// Identifies the IsBackButtonEnabled dependency property.
        /// </summary>
        public static readonly DependencyProperty IsBackButtonEnabledProperty =
                    DependencyProperty.Register(nameof(IsBackButtonEnabled), typeof(bool), typeof(NavigationView), new PropertyMetadata(false));

        /// <summary>
        /// Identifies the BackButtonVisibility dependency property.
        /// </summary>
        public static readonly DependencyProperty BackButtonVisibilityProperty =
                    DependencyProperty.Register(nameof(BackButtonVisibility), typeof(Visibility), typeof(NavigationView), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Identifies the PaneToggleButtonVisibility dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneToggleButtonVisibilityProperty =
                    DependencyProperty.Register(nameof(PaneToggleButtonVisibility), typeof(Visibility), typeof(NavigationView), new PropertyMetadata(Visibility.Visible));

        #endregion

        #region Callbacks

        private static void SelectedItemChanged_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue && d is NavigationView nv)
            {
                if (e.NewValue == null)
                {
                    if (nv.menuItems != null)
                        nv.menuItems.SelectedItem = null;
                    if (nv.footerItems != null)
                        nv.footerItems.SelectedItem = null;
                }
                else
                {
                    if (nv.menuItems != null)
                    {
                        foreach (var item in nv.menuItems.Items)
                        {
                            if (item == e.NewValue)
                            {
                                nv.menuItems.SelectedItem = item;
                                return;
                            }
                        }
                    }
                    if (nv.footerItems != null)
                    {
                        foreach (var item in nv.footerItems.Items)
                        {
                            if (item == e.NewValue)
                            {
                                nv.footerItems.SelectedItem = item;
                                return;
                            }
                        }
                    }
                }
            }
        }

        private static void IsPaneOpenChanged_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue && d is NavigationView nv)
            {
                nv.UpdateVisualState();
            }
        }

        #endregion

        #region Methods

        Rectangle _titleBarRectangle;
        NavigationViewList menuItems, footerItems;
        Button _backButton, _paneToggleButton;
        SplitView _splitView;

        protected override void OnApplyTemplate()
        {
            RemoveHandlers();

            _backButton = (Button)GetTemplateChild("BackButton");
            _paneToggleButton = (Button)GetTemplateChild("PaneToggleButton");
            _titleBarRectangle = (Rectangle)GetTemplateChild("TitleBarDrawableRectangle");
            menuItems = (NavigationViewList)GetTemplateChild("MenuItems");
            footerItems = (NavigationViewList)GetTemplateChild("FooterItems");
            _splitView = (SplitView)GetTemplateChild("RootSplitView");

            AddHandlers();

            //this.TemplateSettings?.Update();
            UpdateVisualState();
            base.OnApplyTemplate();
        }

        private void RemoveHandlers()
        {
            if (_backButton != null)
            {
                _backButton.Click -= BackButton_Click;
            }
            if (_paneToggleButton != null)
            {
                _paneToggleButton.Click -= PaneToggleButton_Click;
            }

            if (menuItems != null)
            {
                menuItems.SelectionChanged -= MenuItems_SelectionChanged;
            }
            if (footerItems != null)
            {
                footerItems.SelectionChanged -= FooterItems_SelectionChanged;
            }

            if (_splitView != null)
            {
                _splitView.PaneOpening -= SplitView_PaneOpening;
                _splitView.PaneClosed -= SplitView_PaneClosed;
            }
        }

        private void AddHandlers()
        {
            if (_backButton != null)
            {
                _backButton.Click += BackButton_Click;
            }
            if (_paneToggleButton != null)
            {
                _paneToggleButton.Click += PaneToggleButton_Click;
            }

            if (menuItems != null)
            {
                menuItems.SelectionChanged += MenuItems_SelectionChanged;
            }
            if (footerItems != null)
            {
                footerItems.SelectionChanged += FooterItems_SelectionChanged;
            }

            if (_titleBarRectangle != null)
            {
                Window.Current?.SetTitleBar(_titleBarRectangle);
            }

            if (_splitView != null)
            {
                _splitView.PaneOpening += SplitView_PaneOpening;
                _splitView.PaneClosed += SplitView_PaneClosed;
            }
        }

        private void UpdateVisualState()
        {
            if (_splitView != null)
            {
                if (_splitView.DisplayMode == SplitViewDisplayMode.CompactInline)
                {
                    VisualStateManager.GoToState(this, "NotOverlaying", false);
                }
                else
                {
                    if (this.IsPaneOpen)
                    {
                        VisualStateManager.GoToState(this, "Overlaying", false);
                    }
                    else
                    {
                        VisualStateManager.GoToState(this, "NotOverlaying", false);
                    }
                }
            }
#if DEBUG
            System.Diagnostics.Debug.WriteLine("IsPaneOpen: " + this.IsPaneOpen);
#endif
            VisualStateManager.GoToState(this, this.IsPaneOpen ? "Opened" : "Closed", true);
        }

        private void SplitView_PaneOpening(SplitView sender, object args)
        {
            UpdateVisualState();
        }

        private void SplitView_PaneClosed(SplitView sender, object args)
        {
            UpdateVisualState();
        }

        private void MenuItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (menuItems.SelectedItem != null)
            {
                //
                footerItems.SelectedItem = null;
                this.SelectedItem = menuItems.SelectedItem;
                this.SelectionChanged?.Invoke(this, new RoutedEventArgs());
            }
            else
            {
                if (footerItems.SelectedItem == null)
                {
                    this.SelectedItem = null;
                    this.SelectionChanged?.Invoke(this, new RoutedEventArgs());
                }
            }
        }

        private void FooterItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (footerItems.SelectedItem != null)
            {
                //
                menuItems.SelectedItem = null;
                this.SelectedItem = footerItems.SelectedItem;
                this.SelectionChanged?.Invoke(this, new RoutedEventArgs());
            }
            else
            {
                if (menuItems.SelectedItem == null)
                {
                    this.SelectedItem = null;
                    this.SelectionChanged?.Invoke(this, new RoutedEventArgs());
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.BackRequested?.Invoke(this, e);
        }

        private void PaneToggleButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsPaneOpen = !this.IsPaneOpen;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the back button receives an interaction such as a click or tap.
        /// </summary>
        public event TypedEventHandler<object, RoutedEventArgs> BackRequested;

        /// <summary>
        /// Occurs when the currently selected item changes.
        /// </summary>
        public event TypedEventHandler<object, RoutedEventArgs> SelectionChanged;

        #endregion
    }
}
