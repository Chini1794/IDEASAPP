using IDEASAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEASAPP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PromocionesPage : ContentPage
	{
		PromocionesViewModel _viewModel;
		public PromocionesPage ()
		{
			InitializeComponent();
			BindingContext = _viewModel = new PromocionesViewModel();
			Hoy.IsChecked = true;
		}

		void OnHoyCheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			_viewModel.Promociones.Clear();
			var check = (sender as RadioButton).IsChecked;
			if (check == true)
			{

				_viewModel.LoadHoyCommand.Execute(this);

			}
		}
		void OnSemanaCheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			_viewModel.Promociones.Clear();
			var check = (sender as RadioButton).IsChecked;
			if (check == true)
			{

				_viewModel.LoadSemanaCommand.Execute(this);
			}
		}
		void OnMesCheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			_viewModel.Promociones.Clear();
			var check = (sender as RadioButton).IsChecked;
			if (check == true)
			{
				_viewModel.LoadMesCommand.Execute(this);

			}
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
		

		}

	}
}