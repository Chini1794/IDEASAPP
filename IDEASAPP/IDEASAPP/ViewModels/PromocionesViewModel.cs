using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
    public class PromocionesViewModel : BaseViewModel
	{
		public ObservableCollection<Promocion> Promociones { get; } 

		public Command LoadHoyCommand { get; }
		public Command LoadSemanaCommand { get; }
		public Command LoadMesCommand { get; }
		public PromocionesViewModel()
        {
			Promociones = new ObservableCollection<Promocion>();
			LoadHoyCommand = new Command(async () => await LoadHoy());
			LoadSemanaCommand = new Command(async () => await LoadSemana());
			LoadMesCommand = new Command(async () => await LoadMes());
		}

		async Task LoadHoy()
		{
			Promociones.Clear();

			var resultado = await PromocionDataStore.GetItemsAsync();
			foreach (var item in resultado.OrderByDescending(x => x.DCreacion))
			{
				if (item.DEstadoActiva)
				{
					if (item.DCreacion >= DateTime.Today) { 
					var negocio = await NegocioMiembroDataStore.GetItemAsync(item.CNegocio);
					item.SourceFoto = ImageSource.FromStream(() => new MemoryStream(item.DFoto));
					item.NombreEmpresa = negocio.DNombreComercial;
				
					Promociones.Add(item);
					}
				}
			}

		}
		async Task LoadSemana()
		{
			Promociones.Clear();

			var resultado = await PromocionDataStore.GetItemsAsync();
			foreach (var item in resultado.OrderByDescending(x => x.DCreacion))
			{
				if (item.DEstadoActiva)
				{
					if (item.DCreacion >= DateTime.Today.AddDays(-7))
					{
						var negocio = await NegocioMiembroDataStore.GetItemAsync(item.CNegocio);
						item.SourceFoto = ImageSource.FromStream(() => new MemoryStream(item.DFoto));
						item.NombreEmpresa = negocio.DNombreComercial;

						Promociones.Add(item);
					}
				}
			}

		}

		async Task LoadMes()
		{
			Promociones.Clear();

			var resultado = await PromocionDataStore.GetItemsAsync();
			foreach (var item in resultado.OrderByDescending(x => x.DCreacion))
			{
				if (item.DEstadoActiva)
				{
					if (item.DCreacion >= DateTime.Today.AddDays(-30))
					{
						var negocio = await NegocioMiembroDataStore.GetItemAsync(item.CNegocio);
						item.SourceFoto = ImageSource.FromStream(() => new MemoryStream(item.DFoto));
						item.NombreEmpresa = negocio.DNombreComercial;

						Promociones.Add(item);
					}
				}
			}

		}
		async Task LoadPromociones()
		{
			Promociones.Clear();

			var resultado = await PromocionDataStore.GetItemsAsync();
			foreach (var item in resultado.OrderByDescending(x => x.DCreacion))
			{
				if (item.DEstadoActiva)
				{
					var negocio = await NegocioMiembroDataStore.GetItemAsync(item.CNegocio);
					item.SourceFoto = ImageSource.FromStream(() => new MemoryStream(item.DFoto));
					item.NombreEmpresa = negocio.DNombreComercial;
					
					Promociones.Add(item);
				}
			}


		}

	}
}
