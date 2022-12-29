using System;
using System.Collections.Generic;
using System.Text;
using MyXamarinFormsApp.Core.ViewModels.Home;

namespace MyXamarinFormsApp.Core.ViewModels
{
    public interface IMediaService
    {
        TimeSpan Position { get; set; }
        AudioFileViewModel CurrentItem { get; set; }
    }

}
