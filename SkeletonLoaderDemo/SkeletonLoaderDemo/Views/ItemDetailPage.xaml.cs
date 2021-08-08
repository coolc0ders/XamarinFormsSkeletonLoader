using SkeletonLoaderDemo.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SkeletonLoaderDemo.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}