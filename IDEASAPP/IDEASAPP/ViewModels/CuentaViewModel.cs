using IDEASAPP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEASAPP.ViewModels
{
    public class CuentaViewModel : BaseViewModel
    {
        public Command PortalCommand { get; }

        public CuentaViewModel()
        {
            PortalCommand = new Command(OnCerrarTapped);
        }

        private async void OnCerrarTapped(object obj)
            
        {

			await Shell.Current.GoToAsync($"//portal");
           

        }
    }
}
