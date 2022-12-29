using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Playback;
using MvvmCross.ViewModels;
using MyXamarinFormsApp.Core.ViewModels.Home;

namespace MyXamarinFormsApp.Core.ViewModels
{
    public class MediaService : MvxNotifyPropertyChanged, IMediaService
    {

        private TimeSpan _position;
        public TimeSpan Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private AudioFileViewModel _currentItem;
        public AudioFileViewModel CurrentItem
        {
            get => _currentItem;
            set => SetProperty(ref _currentItem, value);
        }

        public MediaService()
        {
            // Subscribe to the PositionChanged event of the MediaManager
            CrossMediaManager.Current.PositionChanged += OnPositionChanged;
        }

        private void OnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            // Update the Position property with the current position of the media
            Position = e.Position;

            // Update the DurationString property of the current item if it exists
            //CurrentItem?.UpdateDurationString(e.Position);
        }
    }

}
