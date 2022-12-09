using IDEASAPP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
        private async void OnStartClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        private async void OnForgotPassword()
        {
            await Shell.Current.GoToAsync($"//{nameof(RegistroPage)}");
        }
    }
}
