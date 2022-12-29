using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyXamarinFormsApp.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomCollectionView : CollectionView
    {
        public CustomCollectionView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty IsBusyBindableProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(CustomCollectionView), propertyChanged: IsBusyPropertyChnaged);

        private static void IsBusyPropertyChnaged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyBindableProperty); }
            set { SetValue(IsBusyBindableProperty, value); }
        }


        protected override void OnBindingContextChanged()
        {
            var a = this.ItemsSource;
            var b = this.ItemTemplate;
            var view = (View)b.CreateContent();
            try
            {
                if (view is Grid)
                {
                    var grid = (Grid)view;
                    var childers = grid.Children;
                }
            }
            catch (Exception ex)
            {
            }
        }



    }
}
