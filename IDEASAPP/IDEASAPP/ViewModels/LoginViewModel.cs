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

namespace IDEASAPP.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Command LoginCommand { get; }
        public Command RegistroCommand { get; }
        public Command BackCommand { get; }
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            BackCommand = new Command(OnBackClicked);
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
                Username = "";
                Password = "";
				await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
			}
			else
			{

				await Application.Current.MainPage.DisplayAlert("Mensaje", "Datos ïnvalidos", "OK");
			}
        }   
        private async void OnBackClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(PortalPage)}");
        }
        private async void OnRegisterClicked()
        {
            await Shell.Current.GoToAsync($"//{nameof(RegistroPage)}");
        }
    }
}
