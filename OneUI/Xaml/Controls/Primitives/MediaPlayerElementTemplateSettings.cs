using Windows.UI.Xaml;

namespace OneUI.Xaml.Controls.Primitives
{
    public sealed class MediaPlayerElementTemplateSettings : DependencyObject
    {
        internal MediaPlayerElementTemplateSettings(MediaPlayerElement mediaElement)
        {
            this.mediaElement = mediaElement;
        }

        #region Properties

        public string StrTitle
        {
            get => (string)GetValue(StrTitleProperty);
        }

        public string StrArtist
        {
            get => (string)GetValue(StrArtistProperty);
        }

        public string StrPosition
        {
            get => (string)GetValue(StrPositionProperty);
        }

        public string StrDuration
        {
            get => (string)GetValue(StrDurationProperty);
        }

        public double DblPosition
        {
            get => (double)GetValue(DblPositionProperty);
        }

        public double DblDuration
        {
            get => (double)GetValue(DblDurationProperty);
        }

        #endregion

        #region Dependency properties

        /// <summary>
        /// Identifies the StrTitle dependency property.
        /// </summary>
        public static readonly DependencyProperty StrTitleProperty =
                    DependencyProperty.Register(nameof(StrTitle), typeof(string), typeof(MediaPlayerElementTemplateSettings), new PropertyMetadata(""));

        /// <summary>
        /// Identifies the StrTitle dependency property.
        /// </summary>
        public static readonly DependencyProperty StrArtistProperty =
                    DependencyProperty.Register(nameof(StrArtist), typeof(string), typeof(MediaPlayerElementTemplateSettings), new PropertyMetadata(""));

        /// <summary>
        /// Identifies the StrTitle dependency property.
        /// </summary>
        public static readonly DependencyProperty StrPositionProperty =
                    DependencyProperty.Register(nameof(StrPosition), typeof(string), typeof(MediaPlayerElementTemplateSettings), new PropertyMetadata(""));

        /// <summary>
        /// Identifies the StrTitle dependency property.
        /// </summary>
        public static readonly DependencyProperty StrDurationProperty =
                    DependencyProperty.Register(nameof(StrDuration), typeof(string), typeof(MediaPlayerElementTemplateSettings), new PropertyMetadata(""));

        /// <summary>
        /// Identifies the StrTitle dependency property.
        /// </summary>
        public static readonly DependencyProperty DblPositionProperty =
                    DependencyProperty.Register(nameof(DblPosition), typeof(double), typeof(MediaPlayerElementTemplateSettings), new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the StrTitle dependency property.
        /// </summary>
        public static readonly DependencyProperty DblDurationProperty =
                    DependencyProperty.Register(nameof(DblDuration), typeof(double), typeof(MediaPlayerElementTemplateSettings), new PropertyMetadata(0d));

        #endregion

        #region Methods

        readonly MediaPlayerElement mediaElement;

        internal void Update()
        {
            SetValue(StrTitleProperty, this.mediaElement.Source.Title + " - " + this.mediaElement.Source.Artist);
            SetValue(StrPositionProperty, this.mediaElement.Player.PlaybackSession.Position.ToString(@"mm\:ss"));
            SetValue(StrDurationProperty, this.mediaElement.Player.PlaybackSession.NaturalDuration.ToString(@"mm\:ss"));
            SetValue(DblDurationProperty, this.mediaElement.Player.PlaybackSession.NaturalDuration.TotalSeconds);
        }

        internal void UpdatePosition()
        {
            SetValue(StrPositionProperty, this.mediaElement.Player.PlaybackSession.Position.ToString(@"mm\:ss"));
        }

        #endregion
    }
}
