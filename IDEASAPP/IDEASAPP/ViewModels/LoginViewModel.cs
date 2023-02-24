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

namespace IDEASAPP.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Command LoginCommand { get; }
        public Command RegistroCommand { get; }
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);

            RegistroCommand = new Command(OnRegisterClicked);

        }

		public event PropertyChangedEventHandler PropertyChanged;


        private string username;

        public string Username { 
            get{ return username; }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }
		private string password;

		public string Password
		{
			get { return password; }
			set
			{
				password = value;
				PropertyChanged(this, new PropertyChangedEventArgs("Password"));
			}
		}
		private async void OnLoginClicked(object obj)
        {

			Login log = new Login
			{
				CIdMiembro = Username,
				DContrasena = Password
			};
			Uri requestUri = new Uri("https://felino.vitalit.co.cr/api/api/Login");
			var client = new HttpClient();
			var json = JsonConvert.SerializeObject(log);
			var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync(requestUri, contentJson);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				PersonaMiembro persona = await GetPersonaUser(Username);
				Application.Current.Properties["idUsuario"] = persona.Id;

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
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/PersonaMiembro/");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<PersonaMiembro>>(content);

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
