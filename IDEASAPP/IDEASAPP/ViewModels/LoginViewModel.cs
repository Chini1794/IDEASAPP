using IDEASAPP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RegistroCommand { get; }
        public Command BackCommand { get; }
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            BackCommand = new Command(OnBackClicked);
            RegistroCommand = new Command(OnForgotPassword);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }   
        private async void OnBackClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(PortalPage)}");
        }
        private async void OnForgotPassword()
        {
            await Shell.Current.GoToAsync($"//{nameof(RegistroPage)}");
        }
    }
}
