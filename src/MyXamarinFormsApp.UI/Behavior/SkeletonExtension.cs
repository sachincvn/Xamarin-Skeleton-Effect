using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MvvmCross.Binding.ExpressionParse.MvxParsedExpression;

namespace MyXamarinFormsApp.UI.Behavior
{
    public class SkeletonExtension : Behavior<VisualElement>
    {
        public static readonly BindableProperty ColorProperty =
    BindableProperty.Create(nameof(Color), typeof(Color), typeof(SkeletonExtension), Color.Default);

        public static readonly BindableProperty ISLoadingProperty =
            BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(SkeletonExtension), true);

        private static async void IsLoadingPropertyChnaged(BindableObject bindable, object oldValue, object newValue)
        {
            var element = (VisualElement)bindable;
            if (element != null)
            {
                if (newValue is bool boolean && boolean == true)
                {
                    element.BackgroundColor = Color.Default;
                }
            }
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public bool IsLoading
        {
            get { return (bool)GetValue(ISLoadingProperty); }
            set { SetValue(ISLoadingProperty, value); }
        }



        protected override void OnAttachedTo(VisualElement element)
        {
            element.BindingContextChanged += OnBindingContextChanged;
            base.OnAttachedTo(element);
        }

        protected override void OnDetachingFrom(VisualElement element)
        {
            element.BindingContextChanged -= OnBindingContextChanged;
            base.OnDetachingFrom(element);
        }

        private async void OnBindingContextChanged(object sender, EventArgs e)
        {
            var element = (VisualElement)sender;
            await element.ShowBackgroundColorAsync(Color, IsLoading);


            var bindingContext = element.BindingContext;
            if (bindingContext == null)
            {
                return;
            }

        }
    }


    public static class ElementExtensions
    {
        public static async Task ShowBackgroundColorAsync(this VisualElement element, Color color, bool isLoading)
        {
            element.BackgroundColor = color;
            Device.StartTimer(TimeSpan.FromSeconds(1.5), () =>
            {
                element.FadeTo(0.5, 750, Easing.CubicInOut).ContinueWith((x) =>
                {
                    element.FadeTo(1, 750, Easing.CubicInOut);
                });
                return true;
            });

            if (!isLoading)
            {
                element.BackgroundColor = Color.Default;
            }
        }
    }

}
