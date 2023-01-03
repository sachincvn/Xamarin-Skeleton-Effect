using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MvvmCross.Binding.ExpressionParse.MvxParsedExpression;

namespace MyXamarinFormsApp.UI.Behavior
{
    public class SkeletonExtension : Behavior<VisualElement>
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(SkeletonExtension), Color.Default);

        public static readonly BindableProperty ISLoadingProperty = BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(SkeletonExtension), true, propertyChanged: OnIsLoadingChanged);

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public bool IsLoading
        {
            get => (bool)GetValue(ISLoadingProperty);
            set => SetValue(ISLoadingProperty, value);
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


        private static async void OnIsLoadingChanged(BindableObject bindable, object oldValue, object newValue)
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

    public static class Extensions
    {
        public static readonly BindableProperty IsLoadingProperty = BindableProperty.CreateAttached("IsLoading", typeof(bool), typeof(Extensions), default(bool), propertyChanged: OnLoadingEnabledChanged);

        public static void SetIsLoading(BindableObject element, bool value)
        {
            element.SetValue(IsLoadingProperty, value);
        }

        public static bool GetIsLoading(BindableObject element)
        {
            return (bool)element.GetValue(IsLoadingProperty);
        }
        public static bool IsFading { get; set; }

        private static void OnLoadingEnabledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is View view) || !(newValue is bool))
            {
                return;
            }
            IsFading = (bool)view.GetValue(IsLoadingProperty);

            view.BackgroundColor = Color.FromHex("#E4E4E4");
            Device.StartTimer(TimeSpan.FromSeconds(1.5), () =>
            {
                var isFading = IsFading;
                view.FadeTo(0.5, 750, Easing.CubicInOut).ContinueWith((x) =>
                {
                    view.FadeTo(1, 750, Easing.CubicInOut);
                });
                return isFading;
            });

            if (!IsFading)
            {
                view.BackgroundColor = Color.Default;
            }
        }

        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.CreateAttached("BackgroundColor", typeof(Color), typeof(Extensions), default(Color), propertyChanged: OnBackgroundColorChanged);

        public static void SetBackgroundColor(BindableObject element, Color value)
        {
            element.SetValue(BackgroundColorProperty, value);
        }

        public static Color GetBackgroundColor(BindableObject element)
        {
            return (Color)element.GetValue(BackgroundColorProperty);
        }

        private static void OnBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is View view))
            {
                return;
            }
            view.BackgroundColor = (Color)newValue;
        }
    }

}
