using IDEASAPP.Models;
using IDEASAPP.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using IDEASAPP.Services;

namespace IDEASAPP.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RegistroCommand { get; }
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);

            RegistroCommand = new Command(OnRegisterClicked);

        }

        private string username;

        public string Username {
			get => username;
			set => SetProperty(ref username, value);
		}
		private string password;

		public string Password
		{
			get => password;
			set => SetProperty(ref password, value);
		}
		private async void OnLoginClicked()
        {
			Login log = new Login
			{
				CIdMiembro = Username,
				DContrasena = Password
			};

			var response = await LoginDataStore.AddItemAsync(log);

			if (response)
			{
				PersonaMiembro persona = await GetPersonaUser(Username);
				Application.Current.Properties["idUsuario"] = persona.Id;
				Application.Current.Properties["sesion"] = "UsuarioMiembro";
				await Application.Current.SavePropertiesAsync();
				Username = "";
                Password = "";
				await Shell.Current.GoToAsync($"//about");
		
			}
			else
			{

				await Application.Current.MainPage.DisplayAlert("Mensaje", "Datos Invalidos", "OK");
			}
        }
		async Task<PersonaMiembro> GetPersonaUser(string user)
		{
			PersonaMiembro persona = new PersonaMiembro();

			var resultado = await PersonaMiembroDataStore.GetItemsAsync();

			foreach (PersonaMiembro obj in resultado) {
				if (obj.CIdMiembro == user)
				{
					persona = obj;
				}			
			}
			return persona;

		}
		private async void OnRegisterClicked()
        {
			await Shell.Current.GoToAsync("RegistroPage");
		}
    }
}
