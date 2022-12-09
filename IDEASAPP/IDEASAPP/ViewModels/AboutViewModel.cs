using IDEASAPP.Views;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {   
        public Command EmpresaCommand { get; }
        public Command BusquedaCommand { get; }

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            EmpresaCommand = new Command(OnEmpresaRecienteTapped);
            BusquedaCommand = new Command(OnBusquedaTapped);

        }

        public ICommand OpenWebCommand { get; }

        private async void OnEmpresaRecienteTapped(object obj)
        {
            await Shell.Current.GoToAsync("EmpresaPage");
        }      

        private async void OnBusquedaTapped(object obj)
        {
            await Shell.Current.GoToAsync("BusquedaPage");
        }
    }
}