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
        public Command LinkCommand { get; }

        public AboutViewModel()
        {
            Title = "About";

            EmpresaCommand = new Command(OnEmpresaRecienteTapped);
            BusquedaCommand = new Command(OnBusquedaTapped);
			LinkCommand = new Command(OnLinkTapped);

        }


		private void OnEmpresaRecienteTapped(object obj)
		{
			if (obj is Frame sl)
			{
				if (sl.BackgroundColor == Color.FromHex("#9EA1A3"))
				{
					sl.BackgroundColor = Color.White;
				}
				else
				{
					sl.BackgroundColor = Color.FromHex("#9EA1A3");
				}
			}
		}

			private async void OnLinkTapped(object obj) {
				await Shell.Current.GoToAsync("EmpresaPage");
			}
        private async void OnBusquedaTapped(object obj)
        {
            await Shell.Current.GoToAsync("BusquedaPage");
        }
    }
}