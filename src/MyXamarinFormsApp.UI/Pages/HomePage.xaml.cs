using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MyXamarinFormsApp.Core.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Skeleton;
using Xamarin.Forms.Xaml;

namespace MyXamarinFormsApp.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class HomePage : MvxContentPage<HomeViewModel>
    {
        private bool _isLoading;
        public bool IsLoading { get => _isLoading; set {  if (_isLoading == value) return; _isLoading = value;  }
        }
        public HomePage()
        {
            InitializeComponent();
            SkeletonAnimation();
        }

        private void SkeletonAnimation()
        {
            //Device.StartTimer(TimeSpan.FromSeconds(1.5), () =>
            //{
            //    Label1.FadeTo(0.5, 750, Easing.CubicInOut).ContinueWith((x) =>
            //    {
            //        Label1.FadeTo(1, 750, Easing.CubicInOut);
            //    });

            //    Label2.FadeTo(0.5, 750, Easing.CubicInOut).ContinueWith((x) =>
            //    {
            //        Label2.FadeTo(1, 750, Easing.CubicInOut);
            //    });

            //    Label3.FadeTo(0.5, 750, Easing.CubicInOut).ContinueWith((x) =>
            //    {
            //        Label3.FadeTo(1, 750, Easing.CubicInOut);
            //    });

            //    Frame1.FadeTo(0.5, 750, Easing.CubicInOut).ContinueWith((x) =>
            //    {
            //        Frame1.FadeTo(1, 750, Easing.CubicInOut);
            //    });

            //    return true;
            //});
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                navigationPage.BarTextColor = Color.White;
                navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            IsLoading = !IsLoading;
        }
    }
}
