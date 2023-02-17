using IDEASAPP.Models;
using IDEASAPP.Views;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
		public ObservableCollection<NegocioMiembro> NegociosRecientes { get; }
		public Command EmpresaCommand { get; }
        public Command BusquedaCommand { get; }
        public Command LinkCommand { get; }
		public Command LoadRecientesCommand { get; }
		public AboutViewModel()
        {
            Title = "About";
			NegociosRecientes = new ObservableCollection<NegocioMiembro>();
			LoadRecientesCommand = new Command(async () => await ExecuteLoadItemsCommand());
			EmpresaCommand = new Command(OnEmpresaRecienteTapped);
            BusquedaCommand = new Command(OnBusquedaTapped);
			LinkCommand = new Command(OnLinkTapped);

        }
		async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;

			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/NegocioMiembro");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			NegociosRecientes.Clear();
			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<NegocioMiembro>>(content);
			
			foreach (var item in resultado)
			{
				NegociosRecientes.Add(item);
			}
			IsBusy = false;
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