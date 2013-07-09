using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops.Signatures.LibVlc.Media;
using Vlc.DotNet.Core.Interops.Signatures.LibVlc.MediaListPlayer;
using Vlc.DotNet.Core.Medias;
using System.ComponentModel;
using Vlc.DotNet.Wpf;
using HireHomeEntertainment.Singletons;
using GalaSoft.MvvmLight.Messaging;
using MVVM;

namespace HireHomeEntertainment.View
{
    /// <summary>
    /// Interaction logic for VlcPlayer.xaml
    /// </summary>
    /// <remarks>
    /// Remember that the WPF example requires VLC 1.2 nightly or later. You might have to update the LibVlcDllsPath
    /// and LibVlcPluginsPath settings in the constructor to match your installation. 
    /// </remarks>
    public partial class MediaPlayer : Page
    {
        #region Properties

        /// <summary>
        /// Used to indicate that the user is currently changing the position (and the position bar shall not be updated). 
        /// </summary>
        private bool myPositionChanging;
        private string _callingPage;
        private bool _allowKeyPressMonitoring;

        #endregion

        #region Constructor / destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VlcPlayer"/> class.
        /// </summary>
        public MediaPlayer(string parameters, string callingPage)
        {
            _callingPage = callingPage;
            Messenger.Default.Register<KeyEventArgs>(this, MainWindow_KeyDown); 

            if (Directory.Exists("C:\\Program Files\\VideoLAN\\VLC"))
            {
                // Set libvlc.dll and libvlccore.dll directory path
                VlcContext.LibVlcDllsPath = @"C:\Program Files\VideoLAN\VLC";

                // Set the vlc plugins directory path
                VlcContext.LibVlcPluginsPath = @"C:\Program Files\VideoLAN\VLC\plugins";
            }
            else
            {
                // Set libvlc.dll and libvlccore.dll directory path
                VlcContext.LibVlcDllsPath = @"C:\Program Files (x86)\VideoLAN\VLC";

                // Set the vlc plugins directory path
                VlcContext.LibVlcPluginsPath = @"C:\Program Files (x86)\VideoLAN\VLC\plugins";
            }

            

            /* Setting up the configuration of the VLC instance.
             * You can use any available command-line option using the AddOption function (see last two options). 
             * A list of options is available at 
             *     http://wiki.videolan.org/VLC_command-line_help
             * for example. */

            // Ignore the VLC configuration file
            VlcContext.StartupOptions.IgnoreConfig = true;

            // Enable file based logging
            VlcContext.StartupOptions.LogOptions.LogInFile = false;

            // Shows the VLC log console (in addition to the applications window)
            VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = false;

            // Set the log level for the VLC instance
            VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Debug;

            // Disable showing the movie file name as an overlay
            VlcContext.StartupOptions.AddOption("--no-video-title-show");

            // Pauses the playback of a movie on the last frame
            //VlcContext.StartupOptions.AddOption("--play-and-pause");

            // Initialize the VlcContext
            VlcContext.Initialize();

            InitializeComponent();

            myVlcControl.VideoProperties.Scale = 2.0f;
            myVlcControl.PositionChanged += VlcControlOnPositionChanged;
            myVlcControl.TimeChanged += VlcControlOnTimeChanged;

            myVlcControl.Media = new PathMedia(parameters);
            myVlcControl.Media.ParsedChanged += MediaOnParsedChanged;
            myVlcControl.Play();
        }           
        
        #endregion

        #region commands

        public static RelayCommand Load { get; set; }

        #endregion

        private void RegisterCommands()
        {
            Load = new RelayCommand(param => OnLoad());
        }

        private void OnLoad()
        {
            _allowKeyPressMonitoring = true;
        }

        #region Control playing

        /// <summary>
        /// Called if the Play button is clicked; starts the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonPlayClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Play();
        }

        /// <summary>
        /// Called if the Pause button is clicked; pauses the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonPauseClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Pause();
        }

        /// <summary>
        /// Called if the Stop button is clicked; stops the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonStopClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Stop();
            sliderPosition.Value = 0;
        }

        /// <summary>
        /// Called if the Open button is clicked; shows the open file dialog to select a media file to play. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonOpenClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Stop();

            if (myVlcControl.Media != null)
            {
                myVlcControl.Media.ParsedChanged -= MediaOnParsedChanged;
            }

            var openFileDialog = new OpenFileDialog
            {
                Title = "Open media file for playback",
                FileName = "MOVIES File",
                Filter = "All files |*.*"
            };

            // Process open file dialog box results
            if (openFileDialog.ShowDialog() != true)
                return;

            textBlockOpen.Visibility = Visibility.Collapsed;

            myVlcControl.Media = new PathMedia(openFileDialog.FileName);
            myVlcControl.Media.ParsedChanged += MediaOnParsedChanged;
            myVlcControl.Play();

            /* Instead of opening a file for playback you can also connect to media streams using
             *     myVlcControl.MOVIES = new LocationMedia(@"http://88.190.232.102:6404");
             *     myVlcControl.Play();
             */
        }

        /// <summary>
        /// Volume value changed by the user. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderVolumeValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myVlcControl.AudioProperties.Volume = Convert.ToInt32(sliderVolume.Value);
        }

        /// <summary>
        /// Mute audio check changed
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void CheckboxMuteCheckedChanged(object sender, RoutedEventArgs e)
        {
            myVlcControl.AudioProperties.IsMute = checkboxMute.IsChecked == true;
        }

        #endregion

        /// <summary>
        /// Called by <see cref="VlcControl.MOVIES"/> when the media information was parsed. 
        /// </summary>
        /// <param name="sender">Event sending media. </param>
        /// <param name="e">VLC event arguments. </param>
        private void MediaOnParsedChanged(MediaBase sender, VlcEventArgs<int> e)
        {
            textBlock.Text = string.Format(
                "Duration: {0:00}:{1:00}:{2:00}",
                myVlcControl.Media.Duration.Hours,
                myVlcControl.Media.Duration.Minutes,
                myVlcControl.Media.Duration.Seconds);

            sliderVolume.Value = myVlcControl.AudioProperties.Volume;
            checkboxMute.IsChecked = myVlcControl.AudioProperties.IsMute;
        }

        /// <summary>
        /// Called by the <see cref="VlcControl"/> when the media position changed during playback.
        /// </summary>
        /// <param name="sender">Event sennding control. </param>
        /// <param name="e">VLC event arguments. </param>
        private void VlcControlOnPositionChanged(VlcControl sender, VlcEventArgs<float> e)
        {
            if (myPositionChanging)
            {
                // User is currently changing the position using the slider, so do not update. 
                return;
            }

            sliderPosition.Value = e.Data;
        }

        private void VlcControlOnTimeChanged(VlcControl sender, VlcEventArgs<TimeSpan> e)
        {
            if (myVlcControl.Media == null)
                return;
            var duration = myVlcControl.Media.Duration;
            textBlock.Text = string.Format(
                "{0:00}:{1:00}:{2:00} / {3:00}:{4:00}:{5:00}",
                e.Data.Hours,
                e.Data.Minutes,
                e.Data.Seconds,
                duration.Hours,
                duration.Minutes,
                duration.Seconds);
        }

        #region Change position

        /// <summary>
        /// Start position changing, prevents updates for the slider by the player. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            myPositionChanging = true;
            myVlcControl.PositionChanged -= VlcControlOnPositionChanged;
        }

        /// <summary>
        /// Stop position changing, re-enables updates for the slider by the player. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            myVlcControl.Position = (float)sliderPosition.Value;
            myVlcControl.PositionChanged += VlcControlOnPositionChanged;

            myPositionChanging = false;
        }

        /// <summary>
        /// Change position when the slider value is updated. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (myPositionChanging)
            {
                myVlcControl.Position = (float)e.NewValue;
            }
            //Update the current position text when it is in pause
            var duration = myVlcControl.Media == null ? TimeSpan.Zero : myVlcControl.Media.Duration;
            var time = TimeSpan.FromMilliseconds(duration.TotalMilliseconds * myVlcControl.Position);
            textBlock.Text = string.Format(
                "{0:00}:{1:00}:{2:00} / {3:00}:{4:00}:{5:00}",
                time.Hours,
                time.Minutes,
                time.Seconds,
                duration.Hours,
                duration.Minutes,
                duration.Seconds);
        }

        #endregion


        private void ButtonPreviousClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Previous();
        }

        private void ButtonNextClick(object sender, RoutedEventArgs e)
        {
            myVlcControl.Next();
        }

        private void ButtonExitMPClick(object sender, RoutedEventArgs e)
        {
            exitMediaPlayer();
        }

        private void exitMediaPlayer()
        {
            if (myVlcControl.IsPlaying)
            {
                myVlcControl.Stop();
            }
            VlcContext.CloseAll();
            _allowKeyPressMonitoring = false;
            PageNavigation.Instance.NavigateBack(_callingPage); 
        }

        private void MainWindow_KeyDown(KeyEventArgs e)
        {
            if (PageNavigation.CurrentPage == _callingPage && _allowKeyPressMonitoring)
            {
                if (e.Key == Key.Left)
                {
                    
                }
                if (e.Key == Key.Right)
                {
                }

                if (e.Key == Key.Enter)
                {
                    
                }
                if (e.Key == Key.Escape)
                {
                    exitMediaPlayer();
                }
            }
        }
    }
}
