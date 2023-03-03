using IDEASAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEASAPP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EvaluadoPage : ContentPage
	{
		EvaluadoViewModel _viewModel;
		public EvaluadoPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new EvaluadoViewModel();
			Enviados.IsChecked = true;

		}
		 void OnEnviadosCheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			_viewModel.Aportes.Clear();
			var check = (sender as RadioButton).IsChecked;
			if (check == true)
			{

				_viewModel.LoadEnviadosCommand.Execute(this);

			}
		}
	 void OnEnEsperaCheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			_viewModel.Aportes.Clear();
			var check = (sender as RadioButton).IsChecked;
			if (check == true)
			{
				
				_viewModel.LoadEnEsperaCommand.Execute(this);
			}
		}
		 void OnRespondidosCheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			_viewModel.Aportes.Clear();
			var check = (sender as RadioButton).IsChecked;
			if (check == true)
			{
				_viewModel.LoadRespondidosCommand.Execute(this);

			}
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			
		}
	}
}