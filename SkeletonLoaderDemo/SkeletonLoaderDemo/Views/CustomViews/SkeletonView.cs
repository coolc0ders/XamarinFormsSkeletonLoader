using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SkeletonLoaderDemo.Views.CustomViews
{
    public class SkeletonView : BoxView
    {
        const string LoadingAnimationName = "LoadingAnimation";

        public static readonly BindableProperty StartColorProperty = BindableProperty.Create("StartColor", typeof(Color), typeof(SkeletonView), default(Color));
        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }

        public static readonly BindableProperty EndColorProperty = BindableProperty.Create("EndColor", typeof(Color), typeof(SkeletonView), default(Color));
        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }
        //TODO: add to videeo description : https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/animation/custom

        public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create("IsLoading", typeof(bool), typeof(SkeletonView), default(bool),
            propertyChanged: (obj, oldValue, newValue) =>
            {
                var currentView = obj as SkeletonView;
                if (!(bool)newValue)
                    currentView.AbortAnimation(LoadingAnimationName);
                else
                {
                    //currentView.RunSkeletonColorAnimation();
                    currentView.RunSkeletonGradientAnimation();
                }
            });
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        void RunSkeletonColorAnimation()
        {
            Func<double, Color> transform = (t) =>
                Color.FromRgba(StartColor.R + t * (EndColor.R - StartColor.R),
                    StartColor.G + t * (EndColor.G - StartColor.G),
                    StartColor.B + t * (EndColor.B - StartColor.B),
                    StartColor.A + t * (EndColor.A - StartColor.A));

            this.Animate<Color>(LoadingAnimationName, transform, (color) => BackgroundColor = color, repeat: () => true, length: 2000);
        }

        void RunSkeletonGradientAnimation()
        {
            Func<double, (Color startColor, Color endColor)> transform = (t) =>
            {
                var startColor = Color.FromRgba(StartColor.R + t * (EndColor.R - StartColor.R),
                    StartColor.G + t * (EndColor.G - StartColor.G),
                    StartColor.B + t * (EndColor.B - StartColor.B),
                    StartColor.A + t * (EndColor.A - StartColor.A));

                var endColor = Color.FromRgba(StartColor.R - t * (EndColor.R - StartColor.R),
                    StartColor.G + t * (StartColor.G - EndColor.G),
                    StartColor.B + t * (StartColor.B - EndColor.B),
                    StartColor.A + t * (StartColor.A - EndColor.A));

                return (startColor, endColor);
            };

            this.Animate<(Color startColor, Color endColor)>(LoadingAnimationName, transform, (color) =>
            {
                Background = new LinearGradientBrush
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 0),
                    GradientStops = new GradientStopCollection
                    {
                        new GradientStop(color.startColor, 0.1f),
                        new GradientStop(color.endColor, 0.0f)
                    }
                };
            }, repeat: () => true, length: 1000);
        }
    }
}
