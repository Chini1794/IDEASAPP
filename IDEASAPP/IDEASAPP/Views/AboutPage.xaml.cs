using IDEASAPP.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEASAPP.Views
{
    public partial class AboutPage : ContentPage
    {
		AboutViewModel _viewModel;
		public AboutPage()
        {
            InitializeComponent();
			BindingContext = _viewModel = new AboutViewModel();
            _viewModel.LoadRecientesCommand.Execute(this);

            RecientesListView.ItemsSource = _viewModel.NegociosRecientes;
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}