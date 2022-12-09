using IDEASAPP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
    public class RegistroViewModel : BaseViewModel
    {
        public Command RegistroCommand { get; }
        public Command PortalCommand { get; }
        public Command BackCommand { get; }
        public RegistroViewModel()
        {
            BackCommand = new Command(OnBackClicked);
            RegistroCommand = new Command(OnRegistroClicked);
            PortalCommand = new Command(OnPortalClicked);
        }

        private async void OnRegistroClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(RegistroPage)}");
        }
        private async void OnBackClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(PortalPage)}");
        }
        private async void OnPortalClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(PortalPage)}");
        }
    }
}
