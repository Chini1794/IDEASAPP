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

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.LoadEmpresasCommand.Execute(this);
			_viewModel.OnAppearing();
		}

    }
}