using IDEASAPP.Models;
using IDEASAPP.Services;
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
		public Command BusquedaCommand { get; }
		public Command LoadEmpresasCommand { get; }
		public Command<NegocioMiembro> ItemTapped { get; }


		public AboutViewModel()
		{
			NegociosRecientes = new ObservableCollection<NegocioMiembro>();
			NegociosPopulares = new ObservableCollection<NegocioMiembro>();
			LoadEmpresasCommand = new Command(async () => await LoadEmpresas());
			BusquedaCommand = new Command(OnBusquedaTapped);
			ItemTapped = new Command<NegocioMiembro>(OnItemSelected);

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

		public void OnAppearing()
		{

			SelectedItem = null;
		}

		async Task LoadEmpresas()
		{
			NegociosRecientes.Clear();
			NegociosPopulares.Clear();

			ObservableCollection<NegocioMiembro> aux = new ObservableCollection<NegocioMiembro>();

			var negociosMiembros = await NegocioMiembroDataStore.GetItemsAsync();


			foreach (var item in negociosMiembros.OrderByDescending(x => x.FechaIngreso).Take(5))
			{
				item.SourceFoto = ImageSource.FromStream(() => new MemoryStream(item.DLogo));
				NegociosRecientes.Add(item);
			}
						
			foreach (var item in negociosMiembros)
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

			var resultado = await AporteDataStore.GetItemsAsync();

			foreach (var item in resultado)
			{
				if (item.CNegocio == empresa)
				{
					contador++;
				}
			}
			return contador;
		}

		private async void OnBusquedaTapped(object obj)
		{
			await Shell.Current.GoToAsync("BusquedaPage");
		}
	}
}