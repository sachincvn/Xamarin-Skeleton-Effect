using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Playback;
using MediaManager.Player;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MyXamarinFormsApp.Core.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyXamarinFormsApp.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class HomePage : MvxContentPage<HomeViewModel>
    {
        private bool IsPlaying { get; set; }
        private long _currentSeekBarValue { get; set; }  
        public HomePage()
        {
            InitializeComponent();
            var a = Hello;
            //var a = SliderSongPlayDisplay.Value;

            //SliderSongPlayDisplay.ValueChanged += SliderSongPlayDisplay_ValueChanged;
            //CrossMediaManager.Current.StateChanged += Current_OnStateChanged;
            //CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            //CrossMediaManager.Current.MediaItemChanged += Current_MediaItemChanged;
        }

        //private void Current_MediaItemChanged(object sender, MediaItemEventArgs e)
        //{
        //}
        //private void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        SetupCurrentMediaPositionData(e.Position);
        //    });
        //}

        //private void SetupCurrentMediaPositionData(TimeSpan position)
        //{
        //    var formattingPattern = @"hh\:mm\:ss";
        //    if (CrossMediaManager.Current.Duration.Hours <= 0)
        //        formattingPattern = @"mm\:ss";

        //    var fullLengthString = CrossMediaManager.Current.Duration.ToString(formattingPattern);
        //    //LabelPositionStatus.Text = $"{currentPlaybackPosition.ToString(formattingPattern)}/{fullLengthString}";

        //    SliderSongPlayDisplay.Value = position.Ticks;
        //    _currentSeekBarValue = position.Ticks;
        //}
        //private void Current_OnStateChanged(object sender, StateChangedEventArgs e)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        SetupCurrentMediaPlayerState(e.State);
        //    });
        //}

        //private void SetupCurrentMediaPlayerState(MediaPlayerState currentPlayerState)
        //{
        //    //LabelPlayerStatus.Text = $"{currentPlayerState.ToString().ToUpper()}";

        //    if (currentPlayerState == MediaManager.Player.MediaPlayerState.Loading)
        //    {
        //        SliderSongPlayDisplay.Value = 0;
        //    }
        //    else if (currentPlayerState == MediaManager.Player.MediaPlayerState.Playing
        //            && CrossMediaManager.Current.Duration.Ticks > 0)
        //    {
        //        var a = CrossMediaManager.Current.Duration.Ticks;
        //        var b = SliderSongPlayDisplay.Maximum;
        //        SliderSongPlayDisplay.Maximum = CrossMediaManager.Current.Duration.Ticks;
        //    }
        //}

        //private async void PlayAudio()
        //{
        //    if (!CrossMediaManager.Current.IsPrepared())
        //    {
        //        var audioUrl = "https://ia800605.us.archive.org/32/items/Mp3Playlist_555/Daughtry-Homeacoustic.mp3";
        //        await CrossMediaManager.Current.Play(audioUrl);
        //    }
        //    else
        //    {
        //        await CrossMediaManager.Current.PlayPause();
        //    }
        //}

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    if (Application.Current.MainPage is NavigationPage navigationPage)
        //    {
        //        navigationPage.BarTextColor = Color.White;
        //        navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
        //    }
        //}

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //}

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    IsPlaying = !IsPlaying;
        //    if (IsPlaying)
        //    {
        //        PlayAudio();
        //    }
        //    else
        //    {
        //        await CrossMediaManager.Current.Pause();
        //    }
        //}

        //private async void SliderSongPlayDisplay_ValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //    var newValue = TimeSpan.FromTicks((long)e.NewValue);
        //    if (e.OldValue != _currentSeekBarValue)
        //    {
        //        SliderSongPlayDisplay.Value = e.NewValue;
        //        await CrossMediaManager.Current.SeekTo(newValue);
        //    }
        //}
    }
}
