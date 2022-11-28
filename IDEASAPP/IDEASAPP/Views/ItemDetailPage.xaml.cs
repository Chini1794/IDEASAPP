using IDEASAPP.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace IDEASAPP.Views
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