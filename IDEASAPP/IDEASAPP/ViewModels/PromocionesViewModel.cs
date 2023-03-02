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
		public Command LoadPromocionesCommand { get; }
		public PromocionesViewModel()
        {
			Promociones = new ObservableCollection<Promocion>();
			LoadPromocionesCommand = new Command(async () => await LoadPromociones());
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
