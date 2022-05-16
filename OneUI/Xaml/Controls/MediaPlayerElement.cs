using Windows.UI.Xaml.Controls.Primitives;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Controls;
using Windows.Media.Playback;
using OneUI.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.Media;
using System;
using Windows.UI.Core;
using Windows.Media.Core;
using Windows.Foundation;
using Windows.UI.Xaml.Input;
using Windows.Storage.Streams;
using OneUI.Xaml.Controls.Primitives;

namespace OneUI.Xaml.Controls
{
    /// <summary>
    /// Represents an object that uses a <see cref="MediaPlayer"/> to render audio the display.
    /// </summary>
    public sealed class MediaPlayerElement : Control
    {
        public MediaPlayerElement()
        {
            this.DefaultStyleKey = typeof(MediaPlayerElement);

            this.Player = new MediaPlayer()
            {
                AutoPlay = true
            };
            this.Timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };

            SetValue(TemplateSettingsProperty, new MediaPlayerElementTemplateSettings(this));
            SetValue(TransportControlsProperty, SystemMediaTransportControls.GetForCurrentView());

            this.Player.CommandManager.IsEnabled = false;
            this.TransportControls.IsEnabled = false;
            this.TransportControls.IsPlayEnabled = true;
            this.TransportControls.IsPauseEnabled = true;

            this.Timer.Tick += Timer_Tick;

            this.Player.MediaOpened += Player_MediaOpened;
            this.Player.MediaEnded += Player_MediaEnded;
            this.Player.MediaFailed += Player_MediaFailed;

            this.TransportControls.ButtonPressed += TransportControls_ButtonPressed;
            this.Player.PlaybackSession.PlaybackStateChanged += Player_PlaybackStateChanged;
        }

        #region Properties

        private SystemMediaTransportControls TransportControls
        {
            get => (SystemMediaTransportControls)GetValue(TransportControlsProperty);
        }

        public IMusicPlayback Source
        {
            get => (IMusicPlayback)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public StorageItemThumbnail Poster
        {
            get => (StorageItemThumbnail)GetValue(PosterProperty);
            set => SetValue(PosterProperty, value);
        }

        public bool Shuffled
        {
            get => (bool)GetValue(ShuffledProperty);
            set => SetValue(ShuffledProperty, value);
        }

        public MediaPlayerElementTemplateSettings TemplateSettings
        {
            get => (MediaPlayerElementTemplateSettings)GetValue(TemplateSettingsProperty);
        }

        private DispatcherTimer Timer { get; }

        internal MediaPlayer Player { get; }

        private bool IsDragging { get; set; }

        #endregion

        #region Dependency properties

        /// <summary>
        /// Identifies the TransportControls dependency property.
        /// </summary>
        public static readonly DependencyProperty TransportControlsProperty =
                    DependencyProperty.Register(nameof(TransportControls), typeof(SystemMediaTransportControls), typeof(MediaPlayerElement), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the PlaybackItem dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
                    DependencyProperty.Register(nameof(Source), typeof(IMusicPlayback), typeof(MediaPlayerElement), new PropertyMetadata(null, SourceChanged_Callback));

        /// <summary>
        /// Identifies the Poster dependency property.
        /// </summary>
        public static readonly DependencyProperty PosterProperty =
                    DependencyProperty.Register(nameof(Poster), typeof(StorageItemThumbnail), typeof(MediaPlayerElement), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the Shuffled dependency property.
        /// </summary>
        public static readonly DependencyProperty ShuffledProperty =
                    DependencyProperty.Register(nameof(Shuffled), typeof(bool), typeof(MediaPlayerElement), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the TemplateSettings dependency property.
        /// </summary>
        public static readonly DependencyProperty TemplateSettingsProperty =
                    DependencyProperty.Register(nameof(TemplateSettings), typeof(MediaPlayerElementTemplateSettings), typeof(MediaPlayerElement), new PropertyMetadata(null));

        #endregion

        #region Callbacks

        private static async void SourceChanged_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && (IMusicPlayback)e.OldValue != (IMusicPlayback)e.NewValue && d is MediaPlayerElement me)
            {
                try
                {
                    me.Source.File ??= await StorageFile.GetFileFromPathAsync(me.Source.Path);
                    me.Source.MediaSource ??= MediaSource.CreateFromStorageFile(me.Source.File);
                    me.Player.Source = me.Source.MediaSource;
                    me.SourceChanged?.Invoke(me, e);
                }
                catch (Exception ex)
                {
                    me.MediaFailed?.Invoke(me, ex);
                }
            }
        }

        #endregion

        #region Methods

        private Slider positionSlider,
                    volumeSlider;
        private Button playPauseButton,
                    fullScreenButton;

        protected override void OnApplyTemplate()
        {
            RemoveHandlers();

            positionSlider = (Slider)GetTemplateChild("SliderPosition");
            volumeSlider = (Slider)GetTemplateChild("SliderVolume");
            playPauseButton = (Button)GetTemplateChild("PlayPauseButton");
            fullScreenButton = (Button)GetTemplateChild("FullScreenButton");

            AddHandlers();

            base.OnApplyTemplate();

            UpdateVisualState("None");
        }

        private void RemoveHandlers()
        {
            if (positionSlider != null)
            {
                positionSlider.ManipulationStarted -= PositionSlider_ManipulationStarted;
                positionSlider.ManipulationCompleted -= PositionSlider_ManipulationCompleted;
            }
            if (volumeSlider != null)
            {
                volumeSlider.ValueChanged -= VolumeSlider_ValueChanged;
            }
            if (playPauseButton != null)
            {
                playPauseButton.Click -= PlayPauseButton_Click;
            }
            if (fullScreenButton != null)
            {
                fullScreenButton.Click -= FullScreenButton_Click;
            }
        }

        private void AddHandlers()
        {
            if (positionSlider != null)
            {
                positionSlider.ManipulationStarted += PositionSlider_ManipulationStarted;
                positionSlider.ManipulationCompleted += PositionSlider_ManipulationCompleted;
            }
            if (volumeSlider != null)
            {
                volumeSlider.ValueChanged += VolumeSlider_ValueChanged;
            }
            if (playPauseButton != null)
            {
                playPauseButton.Click += PlayPauseButton_Click;
            }
            if (fullScreenButton != null)
            {
                fullScreenButton.Click += FullScreenButton_Click;
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            this.Player.Volume = e.NewValue;
        }

        private void PositionSlider_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            IsDragging = true;
        }

        private void PositionSlider_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (positionSlider != null)
            {
                this.Player.PlaybackSession.Position = TimeSpan.FromSeconds(positionSlider.Value);
            }
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            switch (this.Player.PlaybackSession.PlaybackState)
            {
                case MediaPlaybackState.Playing:
                    this.Pause();
                    break;
                case MediaPlaybackState.Paused:
                    this.Play();
                    break;
            }
        }

        public void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void RepeatButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            this.FullScreenRequested?.Invoke(this, e);
        }

        private void Timer_Tick(object sender, object e)
        {
            if (!this.IsDragging)
            {
                this.TemplateSettings?.UpdatePosition();
                positionSlider.Value = this.Player.PlaybackSession.Position.TotalSeconds;
            }
        }

        private async void Player_MediaOpened(object sender, object e)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Low, async () =>
            {
                if (!this.TransportControls.IsEnabled)
                {
                    this.TransportControls.IsEnabled = true;
                }

                this.TemplateSettings?.Update();

                this.Poster = await this.Source.File.GetThumbnailAsync(ThumbnailMode.MusicView, 150);

                var du = this.TransportControls.DisplayUpdater;
                du.Type = MediaPlaybackType.Music;
                du.MusicProperties.Title = this.Source.Title;
                du.MusicProperties.Artist = this.Source.Artist;
                du.Thumbnail = RandomAccessStreamReference.CreateFromStream(this.Poster);
                du.Update();

                this.MediaOpened?.Invoke(this, e);
            });
        }

        private async void Player_MediaEnded(object sender, object e)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                if (this.TransportControls.IsEnabled)
                {
                    this.TransportControls.IsEnabled = false;
                }
                UpdateVisualState("None");
                this.MediaEnded?.Invoke(this, e);
            });
        }

        private async void Player_MediaFailed(object sender, object e)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                this.MediaFailed?.Invoke(this, e);
            });
        }

        private async void Player_PlaybackStateChanged(MediaPlaybackSession sender, object e)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                switch (sender.PlaybackState)
                {
                    case MediaPlaybackState.Playing:
                        this.TransportControls.PlaybackStatus = MediaPlaybackStatus.Playing;
                        this.Timer.Start();
                        break;
                    case MediaPlaybackState.Paused:
                        this.TransportControls.PlaybackStatus = MediaPlaybackStatus.Paused;
                        this.Timer.Stop();
                        break;
                    case MediaPlaybackState.Buffering:
                        this.TransportControls.PlaybackStatus = MediaPlaybackStatus.Changing;
                        this.Timer.Stop();
                        break;
                    case MediaPlaybackState.Opening:
                        this.TransportControls.PlaybackStatus = MediaPlaybackStatus.Changing;
                        this.Timer.Stop();
                        break;
                    case MediaPlaybackState.None:
                        this.TransportControls.PlaybackStatus = MediaPlaybackStatus.Closed;
                        this.Timer.Stop();
                        break;
                }
                UpdateVisualState(sender.PlaybackState.ToString());

                this.PlaybackStateChanged?.Invoke(this, e);
            });
        }

        private async void TransportControls_ButtonPressed(object sender, SystemMediaTransportControlsButtonPressedEventArgs e)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                switch (e.Button)
                {
                    case SystemMediaTransportControlsButton.Play:
                        Play();
                        break;
                    case SystemMediaTransportControlsButton.Pause:
                        Pause();
                        break;
                }
            });
        }

        private void UpdateVisualState(string stateName, bool useTransition = false)
        {
            VisualStateManager.GoToState(this, stateName, useTransition);
        }

        // <-----------------------------------------------------------------------------> //

        public bool Play()
        {
            if (this.Player.PlaybackSession.PlaybackState == MediaPlaybackState.Paused)
            {
                this.Player.Play();
                return true;
            }
            return false;
        }

        public bool Pause()
        {
            if (this.Player.PlaybackSession.PlaybackState == MediaPlaybackState.Playing)
            {
                this.Player.Pause();
                return true;
            }
            return false;
        }

        #endregion

        #region Events

        public event TypedEventHandler<object, object> MediaOpened;

        public event TypedEventHandler<object, object> MediaEnded;

        public event TypedEventHandler<object, object> MediaFailed;

        public event TypedEventHandler<object, object> PlaybackStateChanged;

        public event TypedEventHandler<object, object> SourceChanged;

        public event TypedEventHandler<object, object> FullScreenRequested;

        #endregion
    }
}

