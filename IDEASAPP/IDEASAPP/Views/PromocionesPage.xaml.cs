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
	public partial class PromocionesPage : ContentPage
	{
		PromocionesViewModel _viewModel;
		public PromocionesPage ()
		{
			InitializeComponent ();
			BindingContext = _viewModel = new PromocionesViewModel();
			_viewModel.LoadPromocionesCommand.Execute(this);
		}
	}
}