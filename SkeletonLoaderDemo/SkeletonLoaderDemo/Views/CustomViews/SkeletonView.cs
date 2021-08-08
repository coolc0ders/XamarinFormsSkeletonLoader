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
                    currentView.RunSkeletonColorAnimation();
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
    }
}
