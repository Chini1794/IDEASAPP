    using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using IDEASAPP.Views;

namespace IDEASAPP.ViewModels
{
    class BusquedaViewModel : BaseViewModel
	{

		public Command EmpresaCommand { get; }

		public BusquedaViewModel()
		{
			EmpresaCommand = new Command(OnEmpresaBusquedaTapped);
		}
		private async void OnEmpresaBusquedaTapped(object obj)
		{
			await Shell.Current.GoToAsync("EmpresaPage");
		}

	}
}
