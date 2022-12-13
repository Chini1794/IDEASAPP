using IDEASAPP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
    class EmpresaViewModel : BaseViewModel
	{

		public Command EvaluarCommand { get; }

		public EmpresaViewModel()
		{
			EvaluarCommand = new Command(OnEvaluarTapped);
		}

		private async void OnEvaluarTapped(object obj)
		{
			await Shell.Current.GoToAsync("ComentariosPage");

		}
	}
}
