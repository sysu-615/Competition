using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Competition.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Video : Page
    {
        MediaPlayer mediaPlayer = new MediaPlayer();
        MediaTimelineController mediaTimelineController = new MediaTimelineController();
        TimeSpan totalTime;
        String playTime;

        DispatcherTimer timer = new DispatcherTimer
        {
            Interval = new TimeSpan(0, 0, 1)
        };

        public Video()
        {
            this.InitializeComponent();
            var mediaSource = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Video.mp4"));
            mediaSource.OpenOperationCompleted += MediaSource_OpenOperationCompleted;
            mediaPlayer.Source = mediaSource;
            mediaPlayer.TimelineController = mediaTimelineController;
            mediaPlayerElement.SetMediaPlayer(mediaPlayer);
            mediaTimelineController.Start();
            PlayButton.Visibility = Visibility.Collapsed;
            timer.Tick += DispatcherTimer_Tick;
            timer.Start();
        }

        private async void MediaSource_OpenOperationCompleted(MediaSource sender, MediaSourceOpenOperationCompletedEventArgs args)
        {
            totalTime = sender.Duration.GetValueOrDefault();
            mediaTimelineController.Position = TimeSpan.FromSeconds(0);
            //timeSlider.Value = mediaTimelineController.Position.TotalSeconds;
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                timeSlider.Minimum = 0;
                timeSlider.Maximum = totalTime.TotalSeconds;
                timeSlider.StepFrequency = 1;
            });
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Visible;
            timer.Start();
            if (mediaTimelineController.State == MediaTimelineControllerState.Paused)
                mediaTimelineController.Resume();
            else
                mediaTimelineController.Start();
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            timeSlider.Value = mediaTimelineController.Position.TotalSeconds;
            playTime = mediaTimelineController.Position.ToString().Substring(0, 8);
            time.Text = playTime + "/" + totalTime.ToString().Substring(0, 8);

            if ((int)timeSlider.Value == (int)timeSlider.Maximum)
                Stop();
        }

        private void Stop()
        {
            mediaTimelineController.Position = TimeSpan.FromSeconds(0);
            mediaTimelineController.Pause();
            PauseButton.Visibility = Visibility.Collapsed;
            PlayButton.Visibility = Visibility.Visible;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            PauseButton.Visibility = Visibility.Collapsed;
            PlayButton.Visibility = Visibility.Visible;

            mediaTimelineController.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void FullButton_Click(object sender, RoutedEventArgs e)
        {
            ApplicationView view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
                view.ExitFullScreenMode();
            else 
                view.TryEnterFullScreenMode();
            //mediaPlayerElement.IsFullWindow = !mediaPlayerElement.IsFullWindow;
        }

        private void Volumn_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            mediaPlayer.Volume = (double)Volumn.Value;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Stop();
        }
    }
}
