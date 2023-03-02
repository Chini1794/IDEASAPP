using IDEASAPP.Models;
using IDEASAPP.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEASAPP.ViewModels
{
    public class PortalViewModel : BaseViewModel
    {
        public Command StartCommand { get; }
        public Command RegistroCommand { get; }
        public Command LoginCommand { get; }
        public PortalViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            StartCommand = new Command(OnStartClicked);
            RegistroCommand = new Command(OnForgotPassword);
        }
        private async void OnLoginClicked(object obj)
        {
			// Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
			await Shell.Current.GoToAsync("//portal/login");
		}

		public async void OnAppearing()
		{

			if (Application.Current.Properties.ContainsKey("idUsuario") == false && Application.Current.Properties.ContainsKey("sesion") == false)
			{
				Application.Current.Properties["idUsuario"] = "";
				Application.Current.Properties["sesion"] = "";
				await Application.Current.SavePropertiesAsync();
			}
			if (!Application.Current.Properties["idUsuario"].Equals("") && !Application.Current.Properties["sesion"].Equals(""))
			{

				await Shell.Current.GoToAsync($"//about");
			}
		}
		private async void OnStartClicked(object obj)
        {

            AnonimoMiembro anonimo = new AnonimoMiembro();
			var anonimoMiembros = await AnonimoMiembroDataStore.GetItemsAsync();
	
			if (anonimoMiembros.Count() == 0) {

				anonimo.CConsecutivo = 1;
			} else {

				anonimo.CConsecutivo = anonimoMiembros.Last().CConsecutivo + 1;
			}
			anonimo.FechaIngreso = DateTime.Now;
			anonimo.DLugar = "N/A";

			var response = await AnonimoMiembroDataStore.AddItemAsync(anonimo);

			if (response)
			{
				anonimoMiembros = await AnonimoMiembroDataStore.GetItemsAsync();
				AnonimoMiembro anonimoAgregado = anonimoMiembros.Last();
				Application.Current.Properties["idUsuario"] = anonimoAgregado.Id;
				Application.Current.Properties["sesion"] = "Anonimo";

				await Application.Current.SavePropertiesAsync();
				await Shell.Current.GoToAsync($"//about");
			}
			else
			{

				await Application.Current.MainPage.DisplayAlert("Mensaje", "Error Inesperado", "OK");
			}


        }

		private async void OnForgotPassword()
        {
			await Shell.Current.GoToAsync("//portal/registro");
		}
    }
}
