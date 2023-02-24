using IDEASAPP.Models;
using IDEASAPP.Views;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
		private NegocioMiembro _selectedItem;
		public ObservableCollection<NegocioMiembro> NegociosRecientes { get; }
		public ObservableCollection<NegocioMiembro> NegociosPopulares { get; }
		public Command EmpresaCommand { get; }
		public Command BusquedaCommand { get; }
		public Command LinkCommand { get; }
		public Command LoadEmpresasCommand { get; }
		public Command<NegocioMiembro> ItemTapped { get; }
		public AboutViewModel()
		{
			Title = "About";
			NegociosRecientes = new ObservableCollection<NegocioMiembro>();
			NegociosPopulares = new ObservableCollection<NegocioMiembro>();
			LoadEmpresasCommand = new Command(async () => await LoadEmpresas());
			EmpresaCommand = new Command(OnEmpresaRecienteTapped);
			BusquedaCommand = new Command(OnBusquedaTapped);
			ItemTapped = new Command<NegocioMiembro>(OnItemSelected);

		}


		public void OnAppearing()
		{

			SelectedItem = null;
		}
		public NegocioMiembro SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				OnItemSelected(value);
			}
		}
		async void OnItemSelected(NegocioMiembro item)
		{
			if (item == null)
				return;

			// This will push the ItemDetailPage onto the navigation stack
			await Shell.Current.GoToAsync($"///about/empresa?{nameof(EmpresaViewModel.EmpresaId)}={item.Id}");
		}


		async Task LoadEmpresas()
		{

			ObservableCollection<NegocioMiembro> aux = new ObservableCollection<NegocioMiembro>();
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/NegocioMiembro");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			NegociosRecientes.Clear();
			NegociosPopulares.Clear();
			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<NegocioMiembro>>(content).OrderByDescending(x => x.FechaIngreso).Take(5);
			var resultado2 = JsonConvert.DeserializeObject<ObservableCollection<NegocioMiembro>>(content);

			foreach (var item in resultado)
			{
				item.SourceFoto = ImageSource.FromStream(() => new MemoryStream(item.DLogo));
				NegociosRecientes.Add(item);
			}
			foreach (var item in resultado2)
			{
				item.SourceFoto = ImageSource.FromStream(() => new MemoryStream(item.DLogo));
				item.NumeroAportes = await NumeroAportes(item.Id);
				aux.Add(item);
			}

			foreach (var item in aux.OrderByDescending(x => x.NumeroAportes)) {
				NegociosPopulares.Add(item);
			}

		}

		private async Task<int> NumeroAportes(int empresa)
		{
			int contador = 0;
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/Aporte");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<Aporte>>(content);
			foreach (var item in resultado)
			{
				if (item.CNegocio == empresa)
				{
					contador++;
				}
			}
			return contador;
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

		private async void OnBusquedaTapped(object obj)
		{
			await Shell.Current.GoToAsync("BusquedaPage");
		}
	}
}