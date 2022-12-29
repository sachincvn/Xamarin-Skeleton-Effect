using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using MediaManager;
using MediaManager.Media;
using MediaManager.Playback;
using MediaManager.Player;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using RestSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MyXamarinFormsApp.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students { get => _students; set => SetProperty(ref _students, value); }

        private bool _isBusy;
        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }

        public IMvxAsyncCommand FetchLocationCommand { get; }
        public IMvxAsyncCommand<AudioFileViewModel> PlayAudioCommand { get; }

        private List<AudioFileViewModel> _audioFiles;

        private AudioFileViewModel _audioFile;

        private bool isPlaying { get; set; }
        private int CurrentItemIndex { get; set; }  
        private string _cureentFilePath { get; set; }
        public List<AudioFileViewModel> AudioFiles { get => _audioFiles; set => SetProperty(ref _audioFiles, value); }

        public HomeViewModel()
        {
            Students = new ObservableCollection<Student>();
            IsBusy = true;
            AudioFiles = new List<AudioFileViewModel>();
            PlayAudioCommand = new MvxAsyncCommand<AudioFileViewModel>(PlayAudioAsync);

            CrossMediaManager.Current.StateChanged += Current_OnStateChanged;
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            InitCollectionView();
        }

        private async Task PlayAudioAsync(AudioFileViewModel item)
        {
            await CrossMediaManager.Current.Pause();
            isPlaying = false;
            CurrentItemIndex = AudioFiles.IndexOf(item);
            await CrossMediaManager.Current.Play(item.AudioFile.FilePath);
            _cureentFilePath = item.AudioFile.FilePath;
            isPlaying = true;
        }

        private void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            _audioFile = AudioFiles[CurrentItemIndex];
            Device.BeginInvokeOnMainThread(() =>
            {
                SetupCurrentMediaPositionData(e.Position);
            });
        }

        private void SetupCurrentMediaPositionData(TimeSpan position)
        {
            var formattingPattern = @"hh\:mm\:ss";
            if (CrossMediaManager.Current.Duration.Hours <= 0)
                formattingPattern = @"mm\:ss";

            var fullLengthString = CrossMediaManager.Current.Duration.ToString(formattingPattern);
            if (isPlaying)
            {
                _audioFile.DurationString = $"{position.ToString(formattingPattern)}/{fullLengthString}";
            }
        }
        private void Current_OnStateChanged(object sender, StateChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SetupCurrentMediaPlayerState(e.State);
            });
        }

        private void SetupCurrentMediaPlayerState(MediaPlayerState currentPlayerState)
        {
            //LabelPlayerStatus.Text = $"{currentPlayerState.ToString().ToUpper()}";

            if (currentPlayerState == MediaManager.Player.MediaPlayerState.Loading)
            {
                //SliderSongPlayDisplay.Value = 0;
                var item = AudioFiles.FirstOrDefault(i => i.AudioFile.FilePath == _cureentFilePath);
                if (item != null)
                    AudioFiles.FirstOrDefault(i => i.AudioFile.FilePath == _cureentFilePath).IsPlaying = "Play";
            }
            else if (currentPlayerState == MediaManager.Player.MediaPlayerState.Playing
                    && CrossMediaManager.Current.Duration.Ticks > 0)
            {
                var item = AudioFiles.FirstOrDefault(i => i.AudioFile.FilePath == _cureentFilePath);
                if (item != null)
                    AudioFiles.ForEach(i => i.IsPlaying = _cureentFilePath == i.AudioFile.FilePath ? "Pause" : "Play");
                //var a = CrossMediaManager.Current.Duration.Ticks;
                //var b = SliderSongPlayDisplay.Maximum;
                //SliderSongPlayDisplay.Maximum = CrossMediaManager.Current.Duration.Ticks;
            }
            else if(currentPlayerState == MediaPlayerState.Stopped)
            {
                var item = AudioFiles.FirstOrDefault(i => i.AudioFile.FilePath == _cureentFilePath);
                if (item != null)
                    AudioFiles.ForEach(i => i.IsPlaying =  "Play");
            }
        }

        private async void InitCollectionView()
        {

        //    AudioFiles = new List<AudioFileViewModel>
        //{
        //    new AudioFileViewModel(new AudioFile { Title = "Audio 1", Duration = 60 , FilePath = "https://download.samplelib.com/mp3/sample-12s.mp3"},null),
        //    new AudioFileViewModel(new AudioFile { Title = "Audio 2", Duration = 120 ,  FilePath = "https://ia800605.us.archive.org/32/items/Mp3Playlist_555/Daughtry-Homeacoustic.mp3"},null),
        //    new AudioFileViewModel(new AudioFile { Title = "Audio 3", Duration = 180 , FilePath  = "https://download.samplelib.com/mp3/sample-15s.mp3"}, null),
        //};
        //    return;
            for (int i = 0; i < 5; i++)
            {
                Students.Add(new Student("Sachin", "Chavan", i, "image_placeholder.png"));
            }
            await Task.Delay(5000);
            Students.Clear();
            IsBusy = false;
            for (int i = 0; i < 5; i++)
            {
                Students.Add(new Student("Sachin", "Chavan", i, "image_placeholder.png"));
            }

        }
    }

    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public Student(string firstName, string lastName, int id, string imageUrl)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            ImageUrl = imageUrl;
        }
    }

    public class AudioFileViewModel : MvxViewModel
    {
        private AudioFile _audioFile;
        public AudioFile AudioFile { get => _audioFile; set => SetProperty(ref _audioFile, value); }

        private string _durationString;
        public string DurationString { get => _durationString; set => SetProperty(ref _durationString, value); }

        private string _isPlaying;
        public string IsPlaying { get => _isPlaying; set => SetProperty(ref _isPlaying, value); }

        public ICommand PlayAudioCommand { get; set; }

        public AudioFileViewModel(AudioFile audioFile, IMediaService mediaService)
        {
            AudioFile = audioFile;
            IsPlaying = "Play";
            DurationString = "00 : 00";
        }
    }
    public class AudioFile
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string FilePath { get; set; }
        public string DurationString { get; set; }
    }
}
